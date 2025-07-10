using UnityEngine;

public class Water : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Planta regada");
            Destroy(other.gameObject, 1f);
            PlantSpawner spawner = GameObject.FindAnyObjectByType<PlantSpawner>();
            spawner.SpawnPlant();
            // Aquí habría que hacer que la planta crezca y que ganemos el juego
        }

        Destroy(gameObject);
    }
}
