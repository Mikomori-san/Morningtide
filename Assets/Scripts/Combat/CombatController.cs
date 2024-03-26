using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> players = new List<GameObject>();

    void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            BaseStats enemyStats = enemies[i].GetComponent<BaseStats>();
            if (enemyStats.Health <= 0)
            {
                Destroy(enemies[i]);
            }
        }

        for(int i = 0; i < players.Count; i++)
        {
            BaseStats playerStats = players[i].GetComponent<BaseStats>();
            if (playerStats.Health <= 0)
            {
                Destroy(players[i]);
            }
        }

        if(enemies.Count == 0)
        {
            SceneManager.LoadScene("Playground");
        }
        else if(players.Count == 0)
        {
            Debug.Log("You lose!");
        }
    }
}
