using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public List<GameObject> players = new List<GameObject>();
    
    private bool playerRound = true;
    private bool playerDefended = false;
    private float aiTimer = 0;
    
    EnemyLookAtPointLogic enemyPointer;
    
    public bool PlayerRound
    {
        get => playerRound;
        set => playerRound = value;
    }

    public void AttackCurrentEnemy()
    {
        enemies[enemyPointer.GetCurrentEnemyIndex()].GetComponent<BaseStats>().GetDamage(players[0].GetComponent<BaseStats>().Attack);
    }

    public void DefendCurrentPlayer()
    {
        players[0].GetComponent<BaseStats>().BuffDefense(5);
        playerDefended = true;
    }
    
    void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        
        enemyPointer = GetComponent<EnemyLookAtPointLogic>();
    }

    void Update()
    {
        if (!playerRound && aiTimer < 2)
        {
            aiTimer += Time.deltaTime;
            
            if(aiTimer > 2)
            {
                aiTimer = 0;
                playerRound = true;
                Debug.Log("Player Round");
                
                if (playerDefended)
                {
                    players[0].GetComponent<BaseStats>().NerfDefense(5);
                    playerDefended = false;
                }
            }
        }
        
        for (int i = 0; i < enemies.Count; i++)
        {
            BaseStats enemyStats = enemies[i].GetComponent<BaseStats>();
            if (enemyStats.Health <= 0 && !enemies[i].GetComponent<Animator>().GetBool("isDying"))
            {
                enemies[i].GetComponent<Animator>().SetBool("isDying", true);
                StartCoroutine(Dying(enemies[i]));
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
            SceneManager.LoadScene("World");
            Cursor.visible = false;
        }
        else if(players.Count == 0)
        {
            Debug.Log("You lose!");
        }
    }

    IEnumerator Dying(GameObject enemy)
    {
        bool isReallyDying = false;
        
        while (true)
        {
            if (enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                !enemy.GetComponent<Animator>().IsInTransition(0) && isReallyDying)
            {
                enemies.Remove(enemy);
                Destroy(enemy);
                break;
            }
            else
            {
                isReallyDying = true;
                enemyPointer.ResetView(enemies);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
