using UnityEngine;

public class PanMovement : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float minAngle = -45f;
    public float maxAngle = 0f;

    float verticalInput;
    float currentAngle = 0f;

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        currentAngle = Mathf.Clamp(
            currentAngle + verticalInput * rotationSpeed * Time.fixedDeltaTime,
            minAngle,
            maxAngle
        );

        transform.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }
}
