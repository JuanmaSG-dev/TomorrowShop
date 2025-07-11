using System.Collections.Generic;
using UnityEngine;

public class BreadTrigger : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform firePoint;

    [Header("Force")]
    public float minThrowForce = 4f;
    public float MaxThrowForce = 5f;

    [Header("Time")]
    public float minLaunchInterval = 1f;
    public float maxLaunchInterval = 2f;

    [Header("Left Side Angles")]
    public float minForceAngleLeftDegrees = 100f;
    public float maxForceAngleLeftDegrees = 190f;

    [Header("Right Side Angles")]
    public float minForceAngleRightDegrees = 80f;
    public float maxForceAngleRightDegrees = -10f;

    private float forceTimer = 0f;
    private float nextForceInterval;

    private bool lastDirectionLeft = true; // dirección anterior
    private int sameDirectionCount = 0;


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
        nextForceInterval = Random.Range(minLaunchInterval, maxLaunchInterval);
    }

    private void ThrowBread()
    {
        bool goLeft;

        if (sameDirectionCount >= 2)
        {
            goLeft = !lastDirectionLeft;
            sameDirectionCount = 0;
        }
        else
        {
            goLeft = Random.value < 0.5f;

            if (goLeft == lastDirectionLeft)
            {
                sameDirectionCount++;
            }
            else
            {
                sameDirectionCount = 1;
                lastDirectionLeft = goLeft;
            }
        }

        float angleDeg = goLeft
            ? Random.Range(minForceAngleLeftDegrees, maxForceAngleLeftDegrees)
            : Random.Range(minForceAngleRightDegrees, maxForceAngleRightDegrees);

        float angleRad = angleDeg * Mathf.Deg2Rad;

        float throwForce = Random.Range(minThrowForce, MaxThrowForce);

        Vector2 force = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * throwForce;

        GameObject bread = Instantiate(blockPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bread.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

}
