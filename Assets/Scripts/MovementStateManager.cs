using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    float horizontalInput;
    float verticalInput;
    [HideInInspector] public Vector3 dir;
    CharacterController controller;
    [SerializeField] float groundYOoffset;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float gravit = -9.81f;
    Vector3 velocity;
    Vector3 spherePos;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }

    void GetDirectionAndMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        dir = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move(dir * moveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOoffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravit * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
