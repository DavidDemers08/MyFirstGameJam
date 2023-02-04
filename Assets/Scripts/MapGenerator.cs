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
        return map;
    }

    private void SmoothMap()
    {
        for (int i = 0; i < smoothAmount; i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                for (int k = 0; k < map.GetLength(1); k++)
                {
                    if (j == 0 || k == 0 || j == map.GetLength(0) - 1 || k == map.GetLength(1) - 1)
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

    private void DrawMap()
    {
        
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                {
                    //gameObjects[i,j] = Instantiate(grassPrefab, new Vector2(i-centerX, j-centerY), Quaternion.identity,tileContainer);
                    groundTilemap.SetTile(new Vector3Int(i - centerX, j - centerY,0), grass);

                    
                }
                else
                {
                    //if (UnityEngine.Random.value < 1)
                    //{
                    //    Instantiate(trees[UnityEngine.Random.Range(0,trees.Length)], new Vector2(i - centerX, j - centerY), Quaternion.identity,tileContainer);
                    //}
                    
                    forestTilemap.SetTile(new Vector3Int(i - centerX, j - centerY, 0), forest);
                    Instantiate(trees[UnityEngine.Random.Range(0, trees.Length)], forestTilemap.GetCellCenterWorld(new Vector3Int(i - centerX, j - centerY, 0)),Quaternion.identity);
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



    // Update is called once per frame
    void Update()
    {

    }
}
