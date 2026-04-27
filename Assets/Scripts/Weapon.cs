using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{  

    // Shooting parameters
    public bool isShooting , readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 0.5f;

    //burst
    public int bulletsPerBurst = 3;
    public int burstBulletLeft;

    // spread
    public float spreadIntensity = 0.1f;

    // Bullet parameters
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
    public float bulletLifetime = 3f;

    //reload 
    public float reloadTime;
    public bool isReloading;
    public int magazineSize, bulletLeft;

    //ui
    public TextMeshProUGUI bulletText;

    private Animator animator;



    public enum ShootingMode
    {
        Single,
        Burst,
        Automatic
    }

    
    public ShootingMode shootingMode = ShootingMode.Single;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletLeft = magazineSize;
    }

    void Update()
    {
        if (shootingMode == ShootingMode.Automatic)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (shootingMode == ShootingMode.Burst || shootingMode == ShootingMode.Single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (isShooting && readyToShoot)
        {
            burstBulletLeft = bulletsPerBurst;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !isReloading)
        {
            Reload();
        }

        if(bulletText != null)
        {
            bulletText.text = bulletLeft + "  ";
        }
    }
    private void Shoot()
    {

        if (bulletLeft <= 0)
        {
            Sounds.Instance.empty.Play();
            return;
        }

        bulletLeft--;
        //animator.SetTrigger("recoil");
        Sounds.Instance.shoting.Play();

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set the bullet's forward direction to the shooting direction
        bullet.transform.forward = shootingDirection;

        //shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        // Destroy the bullet after a certain time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifetime));

        //check end of burst
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //burst shooting
        if (shootingMode == ShootingMode.Burst && burstBulletLeft > 1)
        {
            burstBulletLeft--;
            Invoke("Shoot", shootingDelay);
        }

    }

    

    private void Reload()
    {   
        
        isReloading = true;
        //animator.SetTrigger("reload");
        Sounds.Instance.reload.Play(); 
        Invoke("FinishReloading", reloadTime);
    }

    private void FinishReloading()
    {
        bulletLeft = magazineSize;
        isReloading = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); // Arbitrary far point
        }

        Vector3 direction = targetPoint - firePoint.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}