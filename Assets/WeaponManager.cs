using UnityEditor.Timeline.Actions;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    WeaponAmmo ammo;
    ActionStateManager actions;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionStateManager>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (ShouldFire()) Fire();
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (actions.currentState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }
    
    void Fire()
    {
        fireRateTimer = 0;
        barrelPosition.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        ammo.currentAmmo--;
        for(int i =0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
