using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumpingPanManager : MonoBehaviour
{
    public static JumpingPanManager instance { get; private set; }

    public Transform pan;

    public GameObject foodPrefab; // Prefab for the food to be spawned
    public Transform[] foodSpawnPoint; // Point where the food will be spawned

    private float cookingProgress = 0f; // Progress of cooking food
    private float cookingTime = 15f; // Time required to cook food
    public Slider cookingBar; // Slider to show cooking progress

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cookingBar.maxValue = 1f;
        cookingBar.value = 0f;
    }

    public void HandleCookingProgress()
    {
        cookingProgress += Time.deltaTime;

        CookFood();

        if (cookingTime <= cookingProgress)
        {
            Debug.Log("Food is ready!");
        }
    }

    private void CookFood()
    {
        // Update the cooking bar slider
        cookingBar.value = cookingProgress / cookingTime;
        // Change the color of the food in the pan based on cooking progress
        foreach (Transform child in pan)
        {
            if (child.CompareTag("Food"))
            {
                float progressRatio = cookingProgress / cookingTime;
                Color foodColor = Color.Lerp(Color.white, Color.HSVToRGB(26, 59, 80), progressRatio);
                child.GetComponent<SpriteRenderer>().color = foodColor;
            }
        }
    }

    public void SpawnFood()
    {
        int randomIndex = Random.Range(0, foodSpawnPoint.Length);
        GameObject food = Instantiate(foodPrefab, foodSpawnPoint[randomIndex].position, Quaternion.identity);
        food.transform.SetParent(pan);
    }

    public void ReduceProgress(float val)
    {
        cookingProgress = Mathf.Max(0, cookingProgress - val);
        cookingBar.value = cookingProgress / cookingTime;
    }
}