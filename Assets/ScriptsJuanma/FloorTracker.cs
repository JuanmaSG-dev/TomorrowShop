using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorTracker : MonoBehaviour
{
    public Tilemap sueloTilemap;
    public Tilemap obstaculosTilemap;
    public float checkInterval = 0.1f;

    private float timer;
    private static HashSet<Vector3Int> paintedTiles = new HashSet<Vector3Int>();
    public static int paintedCount = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckCurrentTile();
        }
    }

    void CheckCurrentTile()
    {
        Vector3Int cell = sueloTilemap.WorldToCell(transform.position);
        if (sueloTilemap.HasTile(cell) && !obstaculosTilemap.HasTile(cell) && !paintedTiles.Contains(cell))
        {
            paintedTiles.Add(cell);
            paintedCount++;
            Debug.Log("Tiles fregados: " + paintedCount);
        }
    }
}
