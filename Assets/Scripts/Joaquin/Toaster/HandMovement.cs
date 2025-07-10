using UnityEngine;

public class HandMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalInput;
    public float speed = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }
}
