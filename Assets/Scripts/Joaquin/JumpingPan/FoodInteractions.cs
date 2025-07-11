using UnityEngine;

public class FoodInteractions : MonoBehaviour
{

    void Update()
    {
        Ray2D ray2D = new Ray2D(transform.position, Vector2.down);

        RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 1f, LayerMask.GetMask("Kitchen"));

        Debug.DrawRay(ray2D.origin, ray2D.direction * 1f, Color.green);
        if (hit.collider != null && hit.collider.CompareTag("Kitchen"))
        {
            JumpingPanManager.instance.CheckCookingProgress();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            JumpingPanManager.instance.ReduceProgress(2f);

            JumpingPanManager.instance.SpawnFood();

            Destroy(gameObject, 1f);
        }
    }
}
