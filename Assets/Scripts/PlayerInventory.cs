using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public bool openInventory = false;
    public GameObject uiInventory;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (!openInventory)
            {
                uiInventory.SetActive(true);
                openInventory = true;
            }

            else
            {
                uiInventory.SetActive(false);
                openInventory = false;
            }

        }
    }
}
