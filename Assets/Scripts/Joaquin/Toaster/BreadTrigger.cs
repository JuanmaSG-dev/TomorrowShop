using System.Collections.Generic;
using UnityEngine;

public class BreadTrigger : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform firePoint;
    public float throwForce = 5f;

    public float minForceInterval = 2f;
    public float maxForceInterval = 3f;

    public float minForceAngleLeftDegrees = 50f;
    public float maxForceAngleLeftDegrees = 120f;

    public float minForceAngleRightDegrees = 50f;
    public float maxForceAngleRightDegrees = 120f;

    private float forceTimer = 0f;
    private float nextForceInterval;

    private void Start()
    {
        SetNextForceInterval();
    }

    void Update()
    {
        forceTimer += Time.deltaTime;
        if (forceTimer >= nextForceInterval)
        {
            ThrowBread();
            forceTimer = 0f;
            SetNextForceInterval();
        }
    }

    private void SetNextForceInterval()
    {
        nextForceInterval = Random.Range(minForceInterval, maxForceInterval);
    }

    private void ThrowBread()
    {
        bool goLeft = Random.value < 0.5f;

        float angleDeg = goLeft
            ? Random.Range(minForceAngleLeftDegrees, maxForceAngleLeftDegrees)
            : Random.Range(minForceAngleRightDegrees, maxForceAngleRightDegrees);

        float angleRad = angleDeg * Mathf.Deg2Rad;
        Vector2 force = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * throwForce;

        GameObject bread = Instantiate(blockPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bread.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

}
