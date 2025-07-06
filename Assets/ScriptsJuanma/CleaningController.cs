using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CleaningController : MonoBehaviour
{
    public Tilemap sueloTilemap;
    public Tilemap obstaculosTilemap;

    public int totalTilesFregables;
    public int tilesFregados;
    private float timer;
    public float checkInterval = 0.2f;

    private HashSet<Vector3Int> tilesFregadosSet = new HashSet<Vector3Int>();

    void Start()
    {
        CalcularTilesFregables();
    }

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
        if (!tilesFregadosSet.Contains(cell) && sueloTilemap.HasTile(cell) && !obstaculosTilemap.HasTile(cell))
        {
            tilesFregadosSet.Add(cell);
            tilesFregados++;

            float porcentaje = (float)tilesFregados / totalTilesFregables * 100f;
            Debug.Log($"Fregado: {tilesFregados}/{totalTilesFregables} ({porcentaje:F1}%)");
        }
    }

    void CalcularTilesFregables()
    {
        BoundsInt bounds = sueloTilemap.cellBounds;
        totalTilesFregables = 0;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (sueloTilemap.HasTile(pos) && !obstaculosTilemap.HasTile(pos))
                {
                    totalTilesFregables++;
                }
            }
        }

        Debug.Log("Tiles fregables totales: " + totalTilesFregables);
    }
}