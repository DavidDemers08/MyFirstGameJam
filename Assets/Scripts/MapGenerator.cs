using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    private int width;
    private int height;
    [Range(0, 100)]
    public int fillPercent;
    public int smoothAmount;
    int centerX, centerY;
    int[,] map;
    GameObject[,] gameObjects;
    private int seed;
    public int borderAmount;
    public GameObject grassPrefab;
    public GameObject forestPrefab;
    public Transform tileContainer;
    public GameObject[] trees;

    public Tilemap groundTilemap;
    public Tilemap forestTilemap;
    public TileBase grass;
    public TileBase forest;

    // Start is called before the first frame update
    void Start()
    {

    }

    public int[,] GenerateMap(int width, int height)
    {
        this.width = width;
        this.height = height;

        centerX = width / 2;
        centerY = height / 2;
        CreateMap();
        SmoothMap();

        DrawMap();
        SpawnExit();
        SpawnTrees();
        return map;
    }

    private void SpawnTrees()
    {
        BoundsInt bounds = forestTilemap.cellBounds;

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tb = forestTilemap.GetTile(new Vector3Int(x-centerX, y-centerY, 0));
                if (tb != null)
                {
                    Instantiate(trees[UnityEngine.Random.Range(0, trees.Length)], forestTilemap.GetCellCenterWorld(new Vector3Int(x - centerX, y - centerY, 0)), Quaternion.identity, tileContainer);
                }
            }
        }
    }

    private void SmoothMap()
    {
        int rng = UnityEngine.Random.Range(0, map.GetLength(1));
        for (int i = 0; i < smoothAmount; i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                for (int k = 0; k < map.GetLength(1); k++)
                {

                    if (j < borderAmount || k < borderAmount || j > map.GetLength(0) - borderAmount - 1 || k > map.GetLength(1) - borderAmount - 1)
                    {
                        map[j, k] = 1;
                    }
                    else
                    {
                        int neighbours = NeighboursCount(j, k);

                        if (neighbours > 4)
                        {
                            map[j, k] = 1;
                        }
                        else if (neighbours < 4)
                        {
                            map[j, k] = 0;
                        }
                    }
                }
            }
        }

    }

    public int NeighboursCount(int idxX, int idxY)
    {
        int neighboursCount = 0;
        for (int i = idxX - 1; i <= idxX + 1; i++)
        {
            for (int j = idxY - 1; j <= idxY + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (i != idxX || j != idxY)
                    {
                        neighboursCount += map[i, j];
                    }
                }
            }
        }

        return neighboursCount;
    }

    public Vector2 SpawnLocationForPlayer()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        Vector3Int randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), bounds.yMin, 0);
        TileBase randomTile = groundTilemap.GetTile(randomCell);
        while (randomTile != grass)
        {
            randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), bounds.yMin, 0);
            randomTile = groundTilemap.GetTile(randomCell);
        }

        return groundTilemap.GetCellCenterWorld(randomCell);
    }
    public Vector3 GetRandomGrassTilePosition()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        Vector3Int randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), UnityEngine.Random.Range(bounds.yMin, bounds.yMax), 0);
        TileBase randomTile = groundTilemap.GetTile(randomCell);
        while (randomTile != grass)
        {
            randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), UnityEngine.Random.Range(bounds.yMin, bounds.yMax), 0);
            randomTile = groundTilemap.GetTile(randomCell);
        }

        return groundTilemap.GetCellCenterWorld(randomCell);
    }

    private void DrawMap()
    {

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                {
                    //gameObjects[i,j] = Instantiate(grassPrefab, new Vector2(i-centerX, j-centerY), Quaternion.identity,tileContainer);
                    groundTilemap.SetTile(new Vector3Int(i - centerX, j - centerY, 0), grass);


                }
                else
                {
                    //if (UnityEngine.Random.value < 1)
                    //{
                    //    Instantiate(trees[UnityEngine.Random.Range(0,trees.Length)], new Vector2(i - centerX, j - centerY), Quaternion.identity,tileContainer);
                    //}

                    forestTilemap.SetTile(new Vector3Int(i - centerX, j - centerY, 0), forest);
                    //gameObjects[i, j] = Instantiate(forestPrefab, new Vector2(i - centerX, j - centerY), Quaternion.identity,tileContainer);
                }
            }
        }
    }

    private void CreateMap()
    {
        map = new int[width, height];
        gameObjects = new GameObject[width, height];
        seed = (int)DateTime.Now.Ticks;
        System.Random random = new System.Random(seed);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {


                map[i, j] = (random.Next(0, 100) < fillPercent) ? 1 : 0;

            }
        }

    }
    public void SpawnExit()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        Vector3Int randomCell = Vector3Int.zero;
        TileBase randomTile = null;
        while (randomTile != grass)
        {
            randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), bounds.yMax - 1, 0);
            randomTile = groundTilemap.GetTile(randomCell);
        }

        Debug.Log("allo");
        for (int i = randomCell.y; i <= randomCell.y + borderAmount; i++)
        {
            forestTilemap.SetTile(new Vector3Int(randomCell.x, i, 0), null);
            groundTilemap.SetTile(new Vector3Int(randomCell.x, i, 0), grass);
        }

    }
}
