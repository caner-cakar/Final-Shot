using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 10;
    [HideInInspector] public WeaponManager weapon;
    [HideInInspector] public Vector3 dir;
    
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
        
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(weapon.damage);

            if (enemyHealth.health <= 0 && !enemyHealth.isDead)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(dir * weapon.enemyKickbackForce, ForceMode.Impulse);
                }
                enemyHealth.isDead = true;
            }
        }
        
        Destroy(gameObject);
    }
}
