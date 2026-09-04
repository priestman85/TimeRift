using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("Weapon Settings")]
    public string weaponName = "AK-47";
    public float damage = 25f;
    public float fireRate = 0.1f;
    public float range = 100f;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 2f;

    [Header("References")]
    public Transform muzzlePoint;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    [Header("Recoil")]
    public float recoilForce = 1f;
    public float recoilRecoverySpeed = 5f;

    float nextFireTime;
    bool isReloading;
    float currentRecoil;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        HandleShooting();
        HandleReload();
        HandleRecoil();
    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !isReloading && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;

        // Muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Sound
        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit, range))
        {
            // Check if we hit an enemy
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Impact effect
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }

        // Apply recoil
        currentRecoil = recoilForce;
    }

    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        if (audioSource != null && reloadSound != null)
            audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reloaded!");
    }

    void HandleRecoil()
    {
        if (currentRecoil > 0)
        {
            currentRecoil = Mathf.Lerp(currentRecoil, 0, recoilRecoverySpeed * Time.deltaTime);
            transform.localPosition -= transform.forward * currentRecoil * Time.deltaTime;
        }
    }
}
