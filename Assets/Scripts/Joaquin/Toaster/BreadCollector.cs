using UnityEngine;
public class BlockCollector : MonoBehaviour
{
    public Transform towerRoot;
    private int blockCount = 0;
    public float blockHeight = 1f;
    public float followSpeed = 2f;

    //TODO: PENSAR BIEN LA LÓGICA PARA RECOGER LOS PANES,
    // SI LA MANO Y EL PLATO COMPARTEN SCRIPT TOWERROOT POSITION NO SE ACTUALIZA
    // SI CAE EN LA MANO NO ACTUALIZA LA POSICION DE PLATO

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COllider");
        if (collision.collider.CompareTag("Food"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            collision.transform.SetParent(towerRoot);
            collision.transform.localPosition = new Vector3(0, blockCount * blockHeight, 0);
            blockCount++;
        }
    }

    void Update()
    {
        towerRoot.position = Vector2.Lerp(towerRoot.position, towerRoot.position, followSpeed * Time.deltaTime);
        
        float stretch = Mathf.Sin(Time.time * 3f) * 0.05f + 1f;
        towerRoot.localScale = new Vector3(1f, stretch, 1f);
    }
}
