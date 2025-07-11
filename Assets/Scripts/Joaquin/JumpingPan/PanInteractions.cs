using System.Collections.Generic;
using UnityEngine;

public class PanInteractions : MonoBehaviour
{
    public float minForceInterval = 2f; // Intervalo mínimo en segundos
    public float maxForceInterval = 4f; // Intervalo máximo en segundos
    public float forceMagnitude = 10f;   // Magnitud de la fuerza

    public Transform friedPoint;

    public float minForceAngleDegrees = 50f;
    public float maxForceAngleDegrees = 120f;

    private float forceTimer = 0f;
    private float nextForceInterval;

    private HashSet<GameObject> alreadyCaptured = new HashSet<GameObject>();

    private void Update()
    {
        if (transform.childCount == 0)
        {
            return;
        }

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
        float angleDeg = Random.Range(minForceAngleDegrees, maxForceAngleDegrees);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 force = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * forceMagnitude;

        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.white;

            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.freezeRotation = true;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
            child.SetParent(null);

            alreadyCaptured.Clear();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food") && !alreadyCaptured.Contains(collision.gameObject))
        {
            alreadyCaptured.Add(collision.gameObject);

            collision.transform.position = friedPoint.position;
            collision.transform.rotation = friedPoint.rotation;

            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.freezeRotation = true;
            }

            collision.transform.SetParent(transform);
        }
    }
}
