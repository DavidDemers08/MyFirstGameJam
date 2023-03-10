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
    public int numberOfChests;

    public Tilemap tilemap;
    public TileBase grassTile;

    // Start is called before the first frame update
    void Start()
    {
        map = mapGenerator.GenerateMap(width, height);
        SpawnPlayer();
        StartCoroutine(SpawnEnemies(numberOfEnnemies));
        SpawnChests(numberOfChests);
    }

    private IEnumerator SpawnEnemies(int numberOfEnnemies)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < numberOfEnnemies; i++)
        {
            Instantiate(EnemyPrefab, mapGenerator.GetRandomGrassTilePosition(), Quaternion.identity,enemyContainer);
        }
    }

    private void SpawnChests(int numberOfChests)
    {
        for (int i = 0; i? < numberOfChests; i++)
        {
            Instantiate(ChestPrefab, mapGenerator.GetRandomGrassTilePosition(), Quaternion.identity, chestContainer);
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, mapGenerator.SpawnLocationForPlayer(), Quaternion.identity);
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
    
}
