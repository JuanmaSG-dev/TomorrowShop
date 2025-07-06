using UnityEngine;

public class PanInteractions : MonoBehaviour
{
    public float minForceInterval = 2f; // Intervalo mínimo en segundos
    public float maxForceInterval = 4f; // Intervalo máximo en segundos
    public float forceMagnitude = 10f;   // Magnitud de la fuerza

    public Transform friedPoint;

    [Header("Ángulo de fuerza (en grados, respecto a la horizontal)")]
    public float minForceAngleDegrees = 50f;
    public float maxForceAngleDegrees = 120f;

    private float forceTimer = 0f;
    private float nextForceInterval;

    private void Start()
    {
        SetNextForceInterval();
    }

    private void Update()
    {
        forceTimer += Time.deltaTime;
        if (forceTimer >= nextForceInterval)
        {
            ApplyForceToChildren();
            forceTimer = 0f;
            SetNextForceInterval();
        }
    }

    private void SetNextForceInterval()
    {
        nextForceInterval = Random.Range(minForceInterval, maxForceInterval);
    }

    private void ApplyForceToChildren()
    {        
        if (transform.childCount == 0)
        {
            return;
        }

        float angleDeg = Random.Range(minForceAngleDegrees, maxForceAngleDegrees);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f);
        Debug.DrawRay(this.transform.position, direction * forceMagnitude, Color.red);


        Vector2 force = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * forceMagnitude;

        foreach (Transform child in transform)
        {
            Debug.Log($"Applying force to child: {child.name}");
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(force, ForceMode2D.Impulse);
                child.SetParent(null);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            // colocar el objeto en la posición y rotacion de friedPoint
            collision.transform.position = friedPoint.position;
            collision.transform.rotation = friedPoint.rotation;

            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.zero;
            }
            collision.transform.SetParent(transform);
        }
    }
}
