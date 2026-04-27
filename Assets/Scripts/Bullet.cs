using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CreateBulletEffect(collision);
            // Destroy the enemy
            print("Enemy hit!" + collision.gameObject.name);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            
            CreateBulletEffect(collision);

            // Destroy the enemy
            print(" hit wall" + collision.gameObject.name);
            //Destroy(collision.gameObject);
        }
        // Destroy the bullet after collision
        //Destroy(gameObject);
    }

    void CreateBulletEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffect ,contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(collision.gameObject.transform);
    }
}
