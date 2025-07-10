using UnityEngine;

public class StretchAnimation : MonoBehaviour
{
    void Update()
    {
        float stretch = Mathf.Sin(Time.time * 3f) * 0.05f + 1f;
        transform.localScale = new Vector3(1f, stretch, 1f);
    }
}
