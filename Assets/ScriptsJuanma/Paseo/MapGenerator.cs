using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 18;
    public int height = 10;

    [Header("Block Size Settings")]
    public int minBlockSize = 2;
    public int maxBlockSize = 4;

    [Header("Prefabs")]
    public GameObject streetTilePrefab;
    public GameObject buildingTilePrefab;
    public GameObject playerHousePrefab;
    public GameObject playerPrefab;
    public GameObject[] collectiblePrefabs;

    private TileType[,] map;
    private List<Vector2Int> walkableTiles = new();
    private List<Vector2Int> buildingTiles = new();
    private Dictionary<Vector2Int, SpriteRenderer> tileVisuals = new();

    void Start()
    {
        GenerateCity();
    }

    void GenerateCity()
    {
        map = new TileType[width, height];
        walkableTiles.Clear();
        buildingTiles.Clear();
        tileVisuals.Clear();

        List<int> streetCols = GenerateStreetLines(width);
        List<int> streetRows = GenerateStreetLines(height);

        if (!streetCols.Contains(0)) streetCols.Insert(0, 0);
        if (!streetCols.Contains(width - 1)) streetCols.Add(width - 1);
        if (!streetRows.Contains(0)) streetRows.Insert(0, 0);
        if (!streetRows.Contains(height - 1)) streetRows.Add(height - 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (streetCols.Contains(x) || streetRows.Contains(y))
                {
                    map[x, y] = TileType.Street;
                    walkableTiles.Add(new Vector2Int(x, y));
                }
                else
                {
                    map[x, y] = TileType.Building;
                    buildingTiles.Add(new Vector2Int(x, y));
                }
            }
        }

        Vector2Int housePos = Vector2Int.zero;
        bool validHouseFound = false;

        foreach (var tile in buildingTiles)
        {
            int streetCount = 0;
            Vector2Int[] directions = {
                new Vector2Int(0, 1), new Vector2Int(1, 0),
                new Vector2Int(0, -1), new Vector2Int(-1, 0)
            };

            foreach (var dir in directions)
            {
                Vector2Int neighbor = tile + dir;
                if (neighbor.x >= 0 && neighbor.y >= 0 &&
                    neighbor.x < width && neighbor.y < height &&
                    map[neighbor.x, neighbor.y] == TileType.Street)
                {
                    streetCount++;
                }
            }

            if (streetCount >= 2)
            {
                housePos = tile;
                validHouseFound = true;
                break;
            }
        }

        if (!validHouseFound)
        {
            Debug.LogError("No se encontró una posición válida para la casa.");
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int posV = new Vector2Int(x, y);
                if (posV == housePos) continue;

                GameObject tile = Instantiate(
                    map[x, y] == TileType.Street ? streetTilePrefab : buildingTilePrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity,
                    transform
                );
                tileVisuals[posV] = tile.GetComponent<SpriteRenderer>();
            }
        }

        Instantiate(playerHousePrefab, new Vector3(housePos.x, housePos.y, 0), Quaternion.identity, transform);
        map[housePos.x, housePos.y] = TileType.Street;
        walkableTiles.Add(housePos);

        PlayerWalker pw = Instantiate(playerPrefab, new Vector3(housePos.x, housePos.y, 0), Quaternion.identity)
            .GetComponent<PlayerWalker>();

        pw.Initialize(housePos, map, tileVisuals);

        List<Vector2Int> used = new();
        for (int i = 0; i < 3; i++)
        {
            Vector2Int pos;
            do
            {
                pos = walkableTiles[Random.Range(0, walkableTiles.Count)];
            } while (used.Contains(pos));

            used.Add(pos);
            GameObject prefab = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
            GameObject go = Instantiate(prefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity, transform);
            pw.RegisterCollectible(pos, go);
        }
    }

    List<int> GenerateStreetLines(int max)
    {
        List<int> lines = new();
        int pos = 0;
        while (pos < max)
        {
            lines.Add(pos);
            pos += Random.Range(minBlockSize, maxBlockSize + 1);
        }
        return lines;
    }
}
