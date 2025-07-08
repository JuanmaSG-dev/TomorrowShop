using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    float horizontalInput;
    float actualDirection = 1f;
    public float speed = 5f; // You can adjust the speed as needed

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        FlipPlayer();

        Rigidbody2D.linearVelocity = new Vector2(horizontalInput, Rigidbody2D.linearVelocityY) * speed;
    }

    private void FlipPlayer()
    {
        if (Mathf.Abs(horizontalInput) > 0.01f && Mathf.Sign(horizontalInput) != actualDirection)
        {
            actualDirection = Mathf.Sign(horizontalInput);
            transform.localScale = new Vector3(actualDirection * -1, 1f, 1f);
        }
    }

}
