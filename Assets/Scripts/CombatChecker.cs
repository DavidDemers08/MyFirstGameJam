using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")){
            collision.gameObject.transform.SetParent(MainManager.Instance.gameObject.transform);
            SceneManager.LoadScene(2);
        }
        
    }
}
