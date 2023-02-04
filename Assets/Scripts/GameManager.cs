using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public int width;
    public int height;
    public int numberOfEnnemies;
    private GameObject[,] gameObjects;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject ChestPrefab;
    public Transform enemyContainer;
    public Transform chestContainer;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = mapGenerator.GenerateMap(width, height);
        SpawnPlayer();
        StartCoroutine(SpawnEnemies(numberOfEnnemies));
        StartCoroutine(SpawnChests(5));
    }

    private IEnumerator SpawnEnemies(int numberOfEnnemies)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < numberOfEnnemies; i++)
        {
            Instantiate(EnemyPrefab, GetRandomGrassTile(), Quaternion.identity,enemyContainer);
        }
    }

    private IEnumerator SpawnChests(int numberOfChests)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i­ < numberOfChests; i++)
        {
            Instantiate(ChestPrefab, GetRandomGrassTile(), Quaternion.identity, chestContainer);
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, GetRandomGrassTile(), Quaternion.identity);
    }

    public Vector3 GetRandomGrassTile()
    {
        
        Tile tile = gameObjects[UnityEngine.Random.Range(0,gameObjects.GetLength(0)), UnityEngine.Random.Range(0,gameObjects.GetLength(1))].GetComponent<Tile>();
        while (!tile.walkable)
        {
             tile = gameObjects[UnityEngine.Random.Range(0, gameObjects.GetLength(0)), UnityEngine.Random.Range(0, gameObjects.GetLength(1))].GetComponent<Tile>();
        }

        return tile.gameObject.transform.position;        
    }
}
