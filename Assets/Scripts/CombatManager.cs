using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerPosition;
    private GameObject player;
    public Slider slider;
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        
        if (player == null) return;
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.transform.position = playerPosition.position;

        InitHealth();
    }

    private void InitHealth()
    {
        slider.value = MainManager.Instance.currentHp / MainManager.Instance.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
