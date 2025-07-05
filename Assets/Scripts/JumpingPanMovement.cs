using UnityEngine;

public class JumpingPanMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float HorizontalInput;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(HorizontalInput, Rigidbody2D.linearVelocityY);
    }

}
