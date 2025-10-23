using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 10;
    float timer;
    void Start()
    {
        
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
