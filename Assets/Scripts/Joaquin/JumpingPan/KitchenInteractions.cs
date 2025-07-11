using Unity.VisualScripting;
using UnityEngine;

public class KitchenInteractions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            JumpingPanManager.instance.ReduceProgress(1f);

            collision.transform.GetComponent<SpriteRenderer>().color = Color.black;

            Destroy(collision.gameObject);

            JumpingPanManager.instance.SpawnFood();
        }
    }
}
