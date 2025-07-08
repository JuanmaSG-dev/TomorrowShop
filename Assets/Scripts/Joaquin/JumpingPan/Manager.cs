using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance { get; private set; }

    public Transform pan;

    public GameObject foodPrefab; // Prefab for the food to be spawned
    public Transform[] foodSpawnPoint; // Point where the food will be spawned

    private float cookingProgress = 0f; // Progress of cooking food
    private float cookingTime = 15f; // Time required to cook food
    public Slider cookingBar; // Slider to show cooking progress

    public TMP_Text timerText;
    public float remainingTime = 60f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerText.text = "00:00";
        cookingBar.maxValue = 1f;
        cookingBar.value = 0f;
    }

    void Update()
    {
        AdvanceTimer();
    }

    private void AdvanceTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            minutes = minutes < 0 ? 0 : minutes;
            seconds = seconds < 0 ? 0 : seconds;

            if (minutes == 0 && seconds == 0)
            {
                timerText.text = "00:00";
                timerText.overflowMode = TextOverflowModes.Overflow;
                timerText.fontSize = 50;
                timerText.color = Color.red;
            }

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
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
        // Spawn food at a random spawn point
        int randomIndex = Random.Range(0, foodSpawnPoint.Length);
        GameObject food = Instantiate(foodPrefab, foodSpawnPoint[randomIndex].position, Quaternion.identity);
        food.transform.SetParent(pan); // Set the pan as the parent of the food
        food.tag = "Food"; // Set the tag for the food
    }

    public void ReduceProgress(float val)
    {
        cookingProgress = Mathf.Max(0, cookingProgress - val);
        cookingBar.value = cookingProgress / cookingTime;
    }
}