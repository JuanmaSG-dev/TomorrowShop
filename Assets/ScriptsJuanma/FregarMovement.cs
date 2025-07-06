using UnityEngine;

public class FregarMovement : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 100f;
    public float currentSpeed = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Clamp(currentSpeed - acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 0.5f);
        }
        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationInput = -1f;
        }
        transform.Rotate(0, 0, rotationInput * rotationSpeed * Time.deltaTime);
        Vector2 moveDirection = transform.up * currentSpeed;
        rb.linearVelocity = moveDirection;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}