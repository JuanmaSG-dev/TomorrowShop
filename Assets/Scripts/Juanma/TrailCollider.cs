using UnityEngine;

public class TrailCollider : MonoBehaviour
{

    public GameObject trailColliderPrefab;
    public float minDistance = 1f;

    private Vector3 lastPosition;
    private bool hasLastPosition = false;

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (!hasLastPosition)
        {
            lastPosition = currentPosition;
            hasLastPosition = true;
            return;
        }

        if (Vector3.Distance(currentPosition, lastPosition) >= minDistance)
        {
            CreateColliderSegment(lastPosition, currentPosition);
            lastPosition = currentPosition;
        }
    }

    void CreateColliderSegment(Vector3 from, Vector3 to)
    {
        Vector3 mid = (from + to) / 2f;
        Vector3 dir = to - from;
        float length = dir.magnitude;

        if (length == 0) return;

        GameObject seg = Instantiate(trailColliderPrefab, mid, Quaternion.identity);
        seg.transform.right = dir.normalized;

        BoxCollider2D col = seg.GetComponent<BoxCollider2D>();
        col.size = new Vector2(length, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trail"))
        {
            Debug.Log("¡PISASTE TRAIL LIMPIO!");
        }
    }

}
