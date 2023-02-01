using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerPosition;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
        if (player == null) return;
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.transform.position = playerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
