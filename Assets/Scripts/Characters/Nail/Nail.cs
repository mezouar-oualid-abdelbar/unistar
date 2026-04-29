using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Nail : MonoBehaviour
{ 
    public float damage = 50f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PractiseTarget target = collision.gameObject.GetComponent<PractiseTarget>();

            if (target != null)
            {
                target.Health -= damage;

                if (target.Health <= 0)
                {
                    Destroy(collision.gameObject);
                }
                Debug.Log("Enemy took damage! " + target.Health);

            }

            Destroy(gameObject); // destroy the nail (bullet)

            Debug.Log("Enemy hit! " + collision.gameObject.name);
            

        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            // stick effect later
            Debug.Log("Hit wall " + collision.gameObject.name);

            Destroy(gameObject); // optional
        }
    } 
}
