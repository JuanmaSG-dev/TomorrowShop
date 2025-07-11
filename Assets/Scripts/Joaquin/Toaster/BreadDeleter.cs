using UnityEngine;

public class BreadDeleter : MonoBehaviour
{
    public Transform towerPoint;

    void Update()
    {
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.2f;

        Vector2 originLeft = new Vector2(transform.position.x - halfWidth, transform.position.y);
        Vector2 originRight = new Vector2(transform.position.x + halfWidth, transform.position.y);

        Vector2 direction = Vector2.down;
        float rayLength = 0.4f;

        CheckAndDestroyFood(
            Physics2D.Raycast(originLeft, direction, rayLength)
            .collider
            );

        CheckAndDestroyFood(
            Physics2D.Raycast(originRight, direction, rayLength)
            .collider
            );

    }

    void CheckAndDestroyFood(Collider2D hit)
    {
        if (hit != null && hit.CompareTag("Food") && !IsChildOfTower(hit.transform))
        {
            Destroy(hit.gameObject);
        }
    }


    bool IsChildOfTower(Transform t)
    {
        return t.IsChildOf(towerPoint);
    }

}
