using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float pushForce; // Force de poussée
    public float minDistance; // Distance maximale pour appliquer la force

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Détection du clic ou du toucher
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Crée un rayon depuis la caméra vers la position du clic
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Destructible"))
                {
                    Rigidbody rb = hit.rigidbody;
                    if (rb != null)
                    {
                        float distance = Vector3.Distance(transform.position, hit.transform.position);

                        if (distance <= minDistance)
                        {
                            Vector3 forceDirection = hit.point - Camera.main.transform.position;
                            forceDirection.Normalize();
                            rb.AddForce(forceDirection * pushForce, ForceMode.Impulse);
                        }
                    }
                }
            }
        }
    }
}