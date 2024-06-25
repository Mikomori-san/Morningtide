using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyLookAtPointLogic : MonoBehaviour
{
    [SerializeField] Transform enemyLookAtPoint;
    [SerializeField] Transform playerLookAtPoint;

    [HideInInspector]
    public List<GameObject> enemies;

    [HideInInspector]
    public Transform currentPlayerTransform;

    private CombatController combatController;

    int CurrentEnemyIndex = 0;

    private bool isStartOfFight = true;

    public int GetCurrentEnemyIndex()
    {
        return CurrentEnemyIndex;
    }
    
    public void SetCurrentEnemyIndex(int index)
    {
        CurrentEnemyIndex = index;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        enemies.Sort((x, y) => HelperFunctions.GetNumberFromName(x.name).CompareTo(HelperFunctions.GetNumberFromName(y.name)));

        currentPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        enemyLookAtPoint.transform.position = currentPlayerTransform.position;
        currentPlayerTransform.LookAt(enemyLookAtPoint.position);
        
        combatController = GetComponent<CombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStartOfFight) return;
        
        if (combatController.PlayerRound)
        {
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                CurrentEnemyIndex = CurrentEnemyIndex == enemies.Count - 1 ? 0 : CurrentEnemyIndex + 1;
            }
            else if(Keyboard.current.aKey.wasPressedThisFrame)
            {
                CurrentEnemyIndex = CurrentEnemyIndex == 0 ? enemies.Count - 1 : CurrentEnemyIndex - 1;
            }
        }
        
        enemyLookAtPoint.transform.position = enemies.ElementAt(CurrentEnemyIndex).transform.position;
        currentPlayerTransform.LookAt(enemyLookAtPoint.position);
    }

    public void SetStartFalse()
    {
        isStartOfFight = false;
        enemyLookAtPoint.transform.position = enemies.ElementAt(CurrentEnemyIndex).transform.position;
        gameObject.GetComponent<UIController>().SetButtonsActive();

        foreach (var enemy in enemies)
        {
            enemy.transform.LookAt(new Vector3(playerLookAtPoint.position.x, enemy.transform.position.y, playerLookAtPoint.position.z));
        }
    }

    public void ResetView(List<GameObject> newEnemies)  // Enemies are getting sorted in CombatController already
    {
        enemies = newEnemies;                                
        enemyLookAtPoint.transform.position = enemies.ElementAt(CurrentEnemyIndex).transform.position;
        currentPlayerTransform.LookAt(enemyLookAtPoint.position);
    }
}
