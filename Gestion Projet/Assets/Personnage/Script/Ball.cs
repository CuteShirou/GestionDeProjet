using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShootSphere : MonoBehaviour
{
    public GameObject spherePrefab; // Assigner un prefab de sph�re dans l'inspecteur
    public float shootForce = 10f; // Force appliqu�e � la sph�re
    public Transform shootPoint; // Point de tir (ex: la position de la cam�ra)
    public float destroyTime = 10f; // Temps avant suppression des sph�res

    private List<GameObject> spheres = new List<GameObject>(); // Liste des sph�res actives

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1 = clic droit
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (spherePrefab == null) return;

        // Obtenir la position et direction de la cam�ra
        Transform cameraTransform = Camera.main.transform;
        Vector3 shootDirection = cameraTransform.forward; // La direction o� regarde la cam�ra

        // Instancier la sph�re
        GameObject sphere = Instantiate(spherePrefab, cameraTransform.position + shootDirection, Quaternion.identity);
        spheres.Add(sphere); // Ajouter � la liste des sph�res actives

        // Ajouter un Rigidbody si ce n'est pas d�j� fait
        Rigidbody rb = sphere.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = sphere.AddComponent<Rigidbody>();
        }

        // Passer en mode de d�tection de collisions continue pour �viter de traverser les murs
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // V�rifier si un SphereCollider est pr�sent, sinon l'ajouter
        if (sphere.GetComponent<SphereCollider>() == null)
        {
            sphere.AddComponent<SphereCollider>();
        }

        // Appliquer une force dans la direction o� regarde la cam�ra
        rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);

        // D�truire automatiquement apr�s un certain temps
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