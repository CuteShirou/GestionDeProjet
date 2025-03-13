using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShootSphere : MonoBehaviour
{
    public GameObject spherePrefab; 
    public float shootForce = 10f; 
    public Transform shootPoint; 
    public float destroyTime = 10f;

    private List<GameObject> spheres = new List<GameObject>(); 

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (spherePrefab == null) return;

        
        Transform cameraTransform = Camera.main.transform;
        Vector3 shootDirection = cameraTransform.forward; 

        
        GameObject sphere = Instantiate(spherePrefab, cameraTransform.position + shootDirection, Quaternion.identity);
        spheres.Add(sphere); 

        
        Rigidbody rb = sphere.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = sphere.AddComponent<Rigidbody>();
        }

        
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        
        if (sphere.GetComponent<SphereCollider>() == null)
        {
            sphere.AddComponent<SphereCollider>();
        }

        
        rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);

        
        StartCoroutine(DestroyAfterTime(sphere, destroyTime));
    }

    IEnumerator DestroyAfterTime(GameObject sphere, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spheres.Contains(sphere))
        {
            spheres.Remove(sphere);
        }
        Destroy(sphere);
    }
}