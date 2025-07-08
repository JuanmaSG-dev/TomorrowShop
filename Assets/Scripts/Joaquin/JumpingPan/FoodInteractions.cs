using UnityEngine;

public class FoodInteractions : MonoBehaviour
{
    public Manager manager;

    void Update()
    {
        Ray2D ray2D = new Ray2D(transform.position, Vector2.down);

        RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, Mathf.Infinity, LayerMask.GetMask("Kitchen"));

        Debug.DrawRay(ray2D.origin, ray2D.direction * 0.5f, Color.green);
        if (hit.collider != null && hit.collider.CompareTag("Kitchen"))
        {
            manager.HandleCookingProgress();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.LogWarning("Collision detected with: " + collision.gameObject.name);
        if (collision.CompareTag("Floor"))
        {
            Debug.LogWarning("Food has fallen to the floor!");

            manager.ReduceProgress(3f);

            gameObject.GetComponent<SphereCollider>().enabled = false;

            gameObject.GetComponent<MeshRenderer>().enabled = false;

            manager.SpawnFood();

            Destroy(gameObject, 1f);
        }
    }
}
