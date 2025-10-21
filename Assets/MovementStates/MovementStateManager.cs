using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed = 3f;
    public float walkSpeed =3f, walkBackSpeed=2f;
    public float runSpeed =7f, runBackSpeed =5f;
    public float crouchSpeed =2f, crouchBackSpeed =1f;
    [HideInInspector]public float horizontalInput;
    [HideInInspector]public float verticalInput;
    [HideInInspector] public Vector3 dir;
    CharacterController controller;
    [SerializeField] float groundYOoffset;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float gravit = -9.81f;
    Vector3 velocity;
    Vector3 spherePos;

    MovementBaseState currentState;
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", horizontalInput);
        anim.SetFloat("vInput", verticalInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        dir = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
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
        if(controller != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
        }
    }
}
