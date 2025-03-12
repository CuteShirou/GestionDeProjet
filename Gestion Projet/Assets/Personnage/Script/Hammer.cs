using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float pushForce; // Force de pouss�e
    public float minDistance; // Distance maximale pour appliquer la force

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // D�tection du clic ou du toucher
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cr�e un rayon depuis la cam�ra vers la position du clic
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