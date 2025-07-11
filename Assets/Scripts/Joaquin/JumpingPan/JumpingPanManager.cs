using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumpingPanManager : MonoBehaviour
{
    public static JumpingPanManager instance { get; private set; }

    public Transform pan;
    public Transform kitchen;

    public GameObject foodPrefab;
    public Transform foodSpawnPoint;
    public float launchAngleDegrees = 70f;
    public float launchForce = 10f;

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
        SpawnFood();
    }

    public void CheckCookingProgress()
    {
        foreach (Transform child in pan)
        {
            if (child.CompareTag("Food"))
            {
                cookingProgress += Time.deltaTime;

                cookingBar.value = cookingProgress / cookingTime;

                float progressRatio = cookingProgress / cookingTime;
                Color foodColor = Color.Lerp(Color.white, Color.HSVToRGB(22, 57, 74), progressRatio);
                child.GetComponent<SpriteRenderer>().color = foodColor;
            }
        }
    }

    public void SpawnFood()
    {
        // Instanciar el prefab
        GameObject food = Instantiate(foodPrefab, foodSpawnPoint.position, Quaternion.identity);

        // Obtener el Rigidbody2D
        Rigidbody2D rb = food.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calcular la dirección desde el ángulo en grados
            float angleRad = launchAngleDegrees * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            // Aplicar la fuerza
            rb.AddForce(forceDirection * launchForce, ForceMode2D.Impulse);
        }
    }

    public void ReduceProgress(float val)
    {
        cookingProgress = Mathf.Max(0, cookingProgress - val);
        cookingBar.value = cookingProgress / cookingTime;
    }
}