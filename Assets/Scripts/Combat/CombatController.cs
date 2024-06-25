using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public List<GameObject> players = new List<GameObject>();
    [HideInInspector] public int enemyNameHasTurn = 1;
    [HideInInspector] public int enemyIndexHasTurn = 0;
    
    private bool playerRound = true;
    private bool playerDefended = false;
    
    EnemyLookAtPointLogic enemyPointer;
    private bool someoneIsDying = false;

    public bool PlayerRound
    {
        get => playerRound;
        set => playerRound = value;
    }

    public void IncreaseEnemyTurn()
    {
        if(enemyIndexHasTurn == enemies.Count - 1)
            enemyIndexHasTurn = 0;
        else
            enemyIndexHasTurn++;
        
        enemyNameHasTurn = int.Parse(enemies[enemyIndexHasTurn].name);
        Debug.Log("Enemy has turn" + enemyNameHasTurn);
    }
    public void AttackCurrentEnemy()
    {
        enemies[enemyPointer.GetCurrentEnemyIndex()].GetComponent<BaseStats>().GetPhysicalDamage(players[0].GetComponent<BaseStats>().Attack);
    }
    
    public void AttackTargetedPlayer(int playerIndex, int damage)
    {
        players[playerIndex].GetComponent<BaseStats>().GetPhysicalDamage(damage);
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
        
        enemies.Sort((x, y) => HelperFunctions.GetNumberFromName(x.name).CompareTo(HelperFunctions.GetNumberFromName(y.name)));
        
        enemyPointer = GetComponent<EnemyLookAtPointLogic>();
    }

    void Update()
    {
        if(enemies.Count == 0 && !someoneIsDying)
        {
            Debug.Log("You win!");
            
            PlayerState.Instance.Health = players[0].GetComponent<BaseStats>().Health;
            PlayerState.Instance.Mana = players[0].GetComponent<BaseStats>().Mana;
            
            Cursor.visible = false;
            SceneManager.LoadScene("World"); 
        }
        
        if(enemies.Count == 0) return;
        
        for (int i = 0; i < enemies.Count; i++)
        {
            BaseStats enemyStats = enemies[i].GetComponent<BaseStats>();
            if (enemyStats.Health <= 0 && !enemies[i].GetComponent<Animator>().GetBool("isDying"))
            {
                enemies[i].GetComponent<Animator>().SetBool("isDying", true);
                
                StartCoroutine(Dying(enemies[i]));
                
                enemies[i].GetComponent<AIBehavior>().enabled = false;
                
                enemies.Remove(enemies[i]);

                enemies.Sort((x, y) => HelperFunctions.GetNumberFromName(x.name).CompareTo(HelperFunctions.GetNumberFromName(y.name)));
                enemyPointer.ResetView(enemies);
                
                enemyPointer.SetCurrentEnemyIndex(0);
                enemyNameHasTurn = int.Parse(enemies[0].name);
            }
        }
        
        for(int i = 0; i < players.Count; i++)
        {
            BaseStats playerStats = players[i].GetComponent<BaseStats>();
            if (playerStats.Health <= 0)
            {
                Debug.Log("You lose!");
                SceneManager.LoadScene("World");
                Cursor.visible = false;
            }
        }
        
        if (!playerRound)
        {
            bool allEnemiesDone = enemies.All(enemy => enemy.GetComponent<AIBehavior>().isDone);

            if (allEnemiesDone)
            {
                playerRound = true;
                enemyNameHasTurn = int.Parse(enemies[0].name);
                Debug.Log("Player Round");
                
                enemyPointer.SetCurrentEnemyIndex(0);
                
                if (playerDefended)
                {
                    players[0].GetComponent<BaseStats>().NerfDefense(5);
                    playerDefended = false;
                }
            }
        }
    }

    IEnumerator Dying(GameObject enemy)
    {
        someoneIsDying = true;
        yield return new WaitForEndOfFrame();
        
        while (true)
        {
            if (enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                !enemy.GetComponent<Animator>().IsInTransition(0))
            {
                Destroy(enemy);
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        someoneIsDying = false;
    }
    
    public void HealPlayer()
    {
        players[0].GetComponent<BaseStats>().Heal(2 * players[0].GetComponent<BaseStats>().Magic);
        players[0].GetComponent<BaseStats>().RemoveMana(30);
    }

    public void MagicMissile()
    {
        enemies[enemyPointer.GetCurrentEnemyIndex()].GetComponent<BaseStats>().GetMagicDamage(players[0].GetComponent<BaseStats>().Magic * 8);
        players[0].GetComponent<BaseStats>().RemoveMana(20);
    }
}
