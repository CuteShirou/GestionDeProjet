using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination; // Destination de téléportation
    [SerializeField] private string teleportTag = "Teleporteur"; // Tag du téléporteur

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clique gauche
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f)) // Distance max du Raycast
            {
                if (hit.collider.CompareTag(teleportTag)) // Vérifie le tag
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");

                    if (player != null && teleportDestination != null)
                    {
                        player.transform.position = teleportDestination.position; // Téléportation
                    }
                }
            }
        }
    }
}

