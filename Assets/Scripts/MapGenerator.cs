using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    [Range(0, 100)]
    public int fillPercent;
    public int smoothAmount;

    public Tilemap tilemapGrass;
    public Tilemap tilemapWater;
    public TileBase lightGreen;
    public TileBase water;
    int centerX, centerY;
    int[,] map;
    private int seed;

    // Start is called before the first frame update
    void Start()
    {
        centerX = width / 2;
        centerY = height / 2;
        GenerateMap();
        SmoothMap();
        DrawMap();
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
                    tilemapGrass.SetTile(new Vector3Int(i - centerX, j - centerY), lightGreen);
                }
                else
                {
                    tilemapWater.SetTile(new Vector3Int(i - centerX, j - centerY), water);
                }
            }
        }
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        seed = (int)System.DateTime.Now.Ticks;
        System.Random random = new(seed);

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
