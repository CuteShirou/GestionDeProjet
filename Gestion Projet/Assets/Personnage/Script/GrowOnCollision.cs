using System.Collections;
using UnityEngine;

public class GrowOnCollision : MonoBehaviour
{
    public string targetTag = "Food"; // Tag de l'objet à manger
    public float growthFactor = 2f;   // Facteur d'agrandissement
    public float duration = 5f;       // Temps avant de revenir à la taille initiale

    private Vector3 originalScale;    // Taille de base du personnage

    private void Start()
    {
        originalScale = transform.localScale; // Sauvegarde la taille initiale
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Vérifie si l'objet touché a le bon tag
        {
            StartCoroutine(GrowTemporarily(other.gameObject));
        }
    }

    private IEnumerator GrowTemporarily(GameObject foodObject)
    {
        transform.localScale *= growthFactor; // Augmente la taille
        foodObject.SetActive(false); // Désactive l'objet mangé
        yield return new WaitForSeconds(duration); // Attend le temps défini
        transform.localScale = originalScale; // Remet à la taille d'origine
    }
}
