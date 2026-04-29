using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailGrenade : MonoBehaviour
{
    public float explosionTime = 3f;
    public float nailSpeed = 5f;

    public GameObject nailPrefab;
    public Transform[] nailHoles = new Transform[1];

    private bool hasExploded = false;

    void Start()
    {
        Invoke("Explode", explosionTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PractiseTarget target = collision.gameObject.GetComponent<PractiseTarget>();

            if (target != null)
            {
                target.Health -= 50;

                if (target.Health <= 0)
                {
                    Destroy(collision.gameObject);
                }
                Debug.Log("Enemy took damage! " + target.Health);

            }
             
            Debug.Log("Enemy hit! " + collision.gameObject.name);

            Explode();

        }
        Explode();
        
    }

    public void Explode()
    { 
        if (hasExploded) return;
        hasExploded = true;

        CancelInvoke("Explode"); 

        for (int i = 0; i < nailHoles.Length; i++)
        { 
            GameObject nail = Instantiate(
                nailPrefab,
                nailHoles[i].position,
                nailHoles[i].rotation
            ); 
            Rigidbody rb = nail.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = nailHoles[i].forward * nailSpeed;
            }
        }

        Destroy(gameObject);
    }
}