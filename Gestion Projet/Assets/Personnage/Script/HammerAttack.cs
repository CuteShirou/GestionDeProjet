using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HammerAttack : MonoBehaviour
{
    private Animator animator;
    public float minDistance = 2.0f; // Distance maximale pour appliquer la force
    public float pushForce = 10f;

    private void Start()
    {
        GameObject hammerMain = GameObject.Find("HammerMain");
        if (hammerMain != null)
        {
            animator = hammerMain.GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Aucun Animator trouvé sur HammerMain !");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animator != null)
            {
                animator.SetTrigger("HammerSlapTrigger");
                Debug.Log("Trigger 'HammerSlapTrigger' activé !");
            }
        }
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