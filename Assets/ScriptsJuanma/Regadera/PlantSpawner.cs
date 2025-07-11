using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject plantPrefab;
    public float minX = -2f, maxX = 7f;
    public float y = -3.5f;

    void Start()
    {
        SpawnPlant();
    }

    public void SpawnPlant()
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), y, 0f);
        Instantiate(plantPrefab, pos, Quaternion.identity);
    }
}

