using UnityEngine;

public class StretchAnimation : MonoBehaviour
{
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float stretch = Mathf.Sin(Time.time * 3f) * 0.05f + 1f; // entre ~0.95 y 1.05
        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * stretch,
            originalScale.z
        );
    }
}
