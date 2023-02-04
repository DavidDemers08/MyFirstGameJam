using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public int width;
    public int height;
    public int numberOfEnnemies;
    private int[,] map;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject ChestPrefab;
    public Transform enemyContainer;
    public Transform chestContainer;

    public Tilemap tilemap;
    public TileBase grassTile;

    // Start is called before the first frame update
    void Start()
    {
        map = mapGenerator.GenerateMap(width, height);
        SpawnPlayer();
        StartCoroutine(SpawnEnemies(numberOfEnnemies));
        StartCoroutine(SpawnChests(5));
    }

    private IEnumerator SpawnEnemies(int numberOfEnnemies)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < numberOfEnnemies; i++)
        {
            Instantiate(EnemyPrefab, GetRandomGrassTilePosition(), Quaternion.identity,enemyContainer);
        }
    }

    private IEnumerator SpawnChests(int numberOfChests)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i­ < numberOfChests; i++)
        {
            Instantiate(ChestPrefab, GetRandomGrassTilePosition(), Quaternion.identity, chestContainer);
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, GetRandomGrassTilePosition(), Quaternion.identity);
    }

    //public Vector3 GetRandomGrassTile()
    //{

    //    Tile tile = gameObjects[UnityEngine.Random.Range(0,gameObjects.GetLength(0)), UnityEngine.Random.Range(0,gameObjects.GetLength(1))].GetComponent<Tile>();
    //    while (!tile.walkable)
    //    {
    //         tile = gameObjects[UnityEngine.Random.Range(0, gameObjects.GetLength(0)), UnityEngine.Random.Range(0, gameObjects.GetLength(1))].GetComponent<Tile>();
    //    }

    //    return tile.gameObject.transform.position;        
    //}
    public Vector3 GetRandomGrassTilePosition()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), UnityEngine.Random.Range(bounds.yMin, bounds.yMax), 0);
        TileBase randomTile = tilemap.GetTile(randomCell);
        while (randomTile != grassTile)
        {
            randomCell = new Vector3Int(UnityEngine.Random.Range(bounds.xMin, bounds.xMax), UnityEngine.Random.Range(bounds.yMin, bounds.yMax), 0);
            randomTile = tilemap.GetTile(randomCell);
        }

        return tilemap.GetCellCenterWorld(randomCell);
    }
}
