using UnityEngine;
public class BlockCollector : MonoBehaviour
{
    public Transform towerRoot;
    private int blockCount = 0;
    public float blockHeight = 1f;
    public float followSpeed = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Food"))
        {
            collision.transform.position = towerRoot.position;
            collision.transform.rotation = towerRoot.rotation;

            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.freezeRotation = true;
            }

            StretchAnimation stretch = collision.collider.GetComponent<StretchAnimation>();
            if (stretch != null)
            {
                stretch.enabled = true;
            }


            collision.transform.SetParent(towerRoot);
            collision.transform.localPosition = new Vector3(0, blockCount * blockHeight, 0);
            blockCount++;
        }
    }

    void Update()
    {
        towerRoot.position = Vector2.Lerp(towerRoot.position, towerRoot.position, followSpeed * Time.deltaTime);
    }
}
