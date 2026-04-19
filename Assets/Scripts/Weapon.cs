using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{ 
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
    public float bulletLifetime = 3f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    private void Shoot()
    { 
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
       bullet .GetComponent<Rigidbody>().AddForce(firePoint.forward.normalized * bulletSpeed, ForceMode.Impulse);
       StartCoroutine( DestroyBulletAfterTime(bullet, bulletLifetime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}