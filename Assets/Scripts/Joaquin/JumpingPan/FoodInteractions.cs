using UnityEngine;

public class FoodInteractions : MonoBehaviour
{

    void Update()
    {
        Ray2D ray2D = new Ray2D(transform.position, Vector2.down);

        RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, Mathf.Infinity, LayerMask.GetMask("Kitchen"));

        Debug.DrawRay(ray2D.origin, ray2D.direction * 0.5f, Color.green);
        if (hit.collider != null && hit.collider.CompareTag("Kitchen"))
        {
            Manager.instance.HandleCookingProgress();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Collision detected with: " + collision.gameObject.name);
        if (collision.CompareTag("Floor"))
        {
            Debug.LogWarning("Food has fallen to the floor!");

            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Manager.instance.ReduceProgress(20f);

            Manager.instance.SpawnFood();

            Destroy(gameObject, 1f);
        }
    }
}
