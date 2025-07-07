using UnityEngine;

public class Manager : MonoBehaviour
{
    public Transform pan;

    private float cookingProgress = 0f; // Progress of cooking food
    private float cookingTime = 5f; // Time required to cook food

    public void HandleCookingProgress()
    {
        cookingProgress += Time.deltaTime;
        Debug.Log($"Cooking Progress: {cookingProgress}");
        CookFood();
        if (cookingTime <= cookingProgress)
        {
            Debug.Log("Food is ready!");
        }
    }

    private void CookFood()
    {
        foreach (Transform child in pan)
        {
            if (child.CompareTag("Food"))
            {
                child.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public void StopCooking()
    {
        foreach (Transform child in pan)
        {
            if (child.CompareTag("Food"))
            {
                child.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }


    //TODO: controlar el tiempo que está sobre estos 2 componentes
    //TODO: Aumentar la barra de "coccion"
    //TODO: Reducir una vida cuando food interactura con floor
}
