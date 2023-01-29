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

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = mapGenerator.GenerateMap(width, height);
        SpawnPlayer();
        StartCoroutine(SpawnEnemies(numberOfEnnemies));
    }

    private IEnumerator SpawnEnemies(int numberOfEnnemies)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < numberOfEnnemies; i++)
        {
            Instantiate(EnemyPrefab, GetRandomGrassTile(), Quaternion.identity);
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(PlayerPrefab, GetRandomGrassTile(), Quaternion.identity);
    }

    public Vector3 GetRandomGrassTile()
    {
        System.Random r = new System.Random();
        GameObject value = gameObjects[r.Next(gameObjects.GetLength(0)), r.Next(gameObjects.GetLength(1))];
        Tile tile = value.GetComponent<Tile>();
        if (tile.walkable)
        {
            return value.transform.position;
        }
        else
        {
            GetRandomGrassTile();
        }

        return new Vector3();
    }
}
