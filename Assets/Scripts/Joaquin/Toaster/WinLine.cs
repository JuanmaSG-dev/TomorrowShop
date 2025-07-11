using UnityEngine;

public class WinLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Debug.Log("Player has won!");
        }
    }
}
