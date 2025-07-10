using UnityEngine;

public class PlateMovement : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    float horizontalInput;
    public float speed = 8f; // You can adjust the speed as needed

    void Start()
    {
        Debug.Log("PlateMovement script started.");
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log("Is pressing input?");
        horizontalInput = Input.GetAxis("Horizontal");
        Debug.Log("value: "+ horizontalInput);
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate called with horizontal input: " + horizontalInput);
        Rigidbody2D.linearVelocity = new Vector2(horizontalInput * speed, Rigidbody2D.linearVelocity.y);
        if (horizontalInput != 0)
        {
            Debug.Log("Moving plate with speed: " + speed);
        }
        else
        {
            Debug.Log("Plate is stationary.");
        }
    }
}
