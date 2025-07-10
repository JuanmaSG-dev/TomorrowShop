using UnityEngine;

public class BreadDeleter : MonoBehaviour
{
    void Update()
    {
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.2f;
        Vector2 originLeft = new Vector2(transform.position.x - halfWidth, transform.position.y);
        Vector2 originRight = new Vector2(transform.position.x + halfWidth, transform.position.y);

        Vector2 direction = Vector2.down;
        float rayLength = 0.5f;

        // Raycast izquierdo
        RaycastHit2D hitLeft = Physics2D.Raycast(originLeft, direction, rayLength);
        Debug.DrawRay(originLeft, direction * rayLength, Color.green);

        // Raycast derecho
        RaycastHit2D hitRight = Physics2D.Raycast(originRight, direction, rayLength);
        Debug.DrawRay(originRight, direction * rayLength, Color.green);

        if (hitLeft.collider != null && hitLeft.collider.CompareTag("Food"))
        {
            Destroy(hitLeft.collider.gameObject);
        }

        if (hitRight.collider != null && hitRight.collider.CompareTag("Food"))
        {
            Destroy(hitRight.collider.gameObject);
        }
    }

}
