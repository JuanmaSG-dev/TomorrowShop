using UnityEngine;
using System.Collections.Generic;

public class PlayerWalker : MonoBehaviour
{
    private Vector2Int currentPos;
    private Vector2Int startPos;
    private TileType[,] map;
    private List<Vector2Int> path = new();

    private Dictionary<Vector2Int, SpriteRenderer> tileVisuals;
    public int moveLimit = 40;
    [SerializeField] private int movesUsed = 0;
    public int collectedCount = 0;
    private Dictionary<Vector2Int, GameObject> collectiblesOnMap = new();
    private HashSet<Vector2Int> collectedPositions = new();

    public void Initialize(Vector2Int start, TileType[,] mapData, Dictionary<Vector2Int, SpriteRenderer> visuals)
    {
        startPos = start;
        currentPos = start;
        map = mapData;
        tileVisuals = visuals;

        path.Clear();
        path.Add(currentPos);
        transform.position = new Vector3(currentPos.x, currentPos.y, 0);

        if (tileVisuals.TryGetValue(currentPos, out var srStart))
            srStart.color = Color.red;
    }

    void Update()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.right;

        if (direction != Vector2Int.zero)
            TryMove(direction);

        if (Input.GetKeyDown(KeyCode.Q))
            UndoMove();
    }

    void TryMove(Vector2Int dir)
    {
        if (movesUsed >= moveLimit)
            return;
        Vector2Int target = currentPos + dir;

        if (target.x < 0 || target.y < 0 || target.x >= map.GetLength(0) || target.y >= map.GetLength(1))
            return;

        if (map[target.x, target.y] != TileType.Street)
            return;

        Vector2Int previousPos = path.Count > 1 ? path[path.Count - 2] : currentPos;

        if (target == previousPos)
            return;

        if (path.Contains(target) && target != startPos)
            return;

        movesUsed++;
        currentPos = target;
        path.Add(currentPos);
        transform.position = new Vector3(currentPos.x, currentPos.y, 0);

        if (tileVisuals.TryGetValue(currentPos, out var sr))
            sr.color = Color.red;
        if (target == startPos && Coleccionable.collectedCount > 0)
        {
            Debug.Log("¡Nivel completado!");
            return;
        }
    }

    void UndoMove()
    {
        if (path.Count > 1)
        {
            Vector2Int lastPos = path[^1];
            movesUsed--;
            if (collectedPositions.Contains(lastPos))
            {
                collectedPositions.Remove(lastPos);
                collectedCount--;

                if (collectiblesOnMap.TryGetValue(lastPos, out var go))
                    go.SetActive(true);
            }
            if (tileVisuals.TryGetValue(currentPos, out var sr))
                sr.color = Color.white;

            path.RemoveAt(path.Count - 1);
            currentPos = path[path.Count - 1];
            transform.position = new Vector3(currentPos.x, currentPos.y, 0);
        }
    }

    public void RegisterCollectible(Vector2Int pos, GameObject collectible)
    {
        collectiblesOnMap[pos] = collectible;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // No quiero hacer muchos tags ahora mismo
        {
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

            if (!collectedPositions.Contains(pos))
            {
                collectedPositions.Add(pos);
                collectedCount++;
                other.gameObject.SetActive(false);
                Debug.Log($"Collected at {pos}. Total: {collectedCount}");
            }
        }
    }
}
