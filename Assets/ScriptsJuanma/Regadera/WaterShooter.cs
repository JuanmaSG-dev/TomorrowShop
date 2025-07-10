using UnityEngine;

public class WaterShooter : MonoBehaviour
{
    public GameObject waterPrefab;
    public Transform shootPoint;
    public LineRenderer lineRenderer;

    public float maxPower = 15f;
    public float powerSpeed = 10f;
    private float currentPower = 0f;

    private bool isCharging = false;
    private bool chargingUp = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isCharging = true;

            if (chargingUp)
            {
                currentPower += powerSpeed * Time.deltaTime;
                if (currentPower >= maxPower)
                {
                    currentPower = maxPower;
                    chargingUp = false;
                }
            }
            else
            {
                currentPower -= powerSpeed * Time.deltaTime;
                if (currentPower <= 0)
                {
                    currentPower = 0;
                    chargingUp = true;
                }
            }

            ShowTrajectory();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
            currentPower = 0f;
            isCharging = false;
            chargingUp = false;
            lineRenderer.positionCount = 0;
        }
    }

    void Shoot()
    {
        GameObject water = Instantiate(waterPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = water.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(1, 1).normalized * currentPower;
    }

    void ShowTrajectory()
    {
        Vector2 velocity = new Vector2(1, 1).normalized * currentPower;
        int steps = 30;
        lineRenderer.positionCount = steps;
        Vector2 pos = shootPoint.position;
        for (int i = 0; i < steps; i++)
        {
            float t = i * 0.1f;
            Vector2 point = pos + velocity * t + 0.5f * Physics2D.gravity * (t * t);
            lineRenderer.SetPosition(i, point);
        }
    }
}
