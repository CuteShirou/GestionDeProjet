using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShootSphere : MonoBehaviour
{
    public GameObject spherePrefab; // Assigner un prefab de sphère dans l'inspecteur
    public float shootForce = 10f; // Force appliquée à la sphère
    public Transform shootPoint; // Point de tir (ex: la position de la caméra)
    public float destroyTime = 10f; // Temps avant suppression des sphères

    private List<GameObject> spheres = new List<GameObject>(); // Liste des sphères actives

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

        // Obtenir la position et direction de la caméra
        Transform cameraTransform = Camera.main.transform;
        Vector3 shootDirection = cameraTransform.forward; // La direction où regarde la caméra

        // Instancier la sphère
        GameObject sphere = Instantiate(spherePrefab, cameraTransform.position + shootDirection, Quaternion.identity);
        spheres.Add(sphere); // Ajouter à la liste des sphères actives

        // Ajouter un Rigidbody si ce n'est pas déjà fait
        Rigidbody rb = sphere.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = sphere.AddComponent<Rigidbody>();
        }

        // Passer en mode de détection de collisions continue pour éviter de traverser les murs
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Vérifier si un SphereCollider est présent, sinon l'ajouter
        if (sphere.GetComponent<SphereCollider>() == null)
        {
            sphere.AddComponent<SphereCollider>();
        }

        // Appliquer une force dans la direction où regarde la caméra
        rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);

        // Détruire automatiquement après un certain temps
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