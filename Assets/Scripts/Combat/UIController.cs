using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject defendButton;
    [SerializeField] private GameObject abilityButton;

    private EnemyLookAtPointLogic enemyPointer;
    private CombatController combatController;
    
    int currentButton = 0;
    const int maxButtons = 2;
    
    private bool areActive = false;

    private bool isDefending;
    
    public void SetButtonsActive()
    {
        attackButton.SetActive(true);
        defendButton.SetActive(true);
        abilityButton.SetActive(true);
        areActive = true;
    }
    void Start()
    {
        enemyPointer = GetComponent<EnemyLookAtPointLogic>();
        combatController = GetComponent<CombatController>();
    }

    void Update()
    {
        if(!areActive) return;
        if(!combatController.PlayerRound) return;
        
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentButton = currentButton == maxButtons ? 0 : currentButton + 1;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentButton = currentButton == 0 ? maxButtons : currentButton - 1;
        }
        
        switch (currentButton)
        {
            case 0:
                attackButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                defendButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                abilityButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                break;
            case 1:
                attackButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                defendButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                abilityButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                break;
            case 2:
                attackButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                defendButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                abilityButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentButton)
            {
                case 0:
                    Debug.Log("Attack");
                    combatController.AttackCurrentEnemy();
                    break;
                case 1:
                    Debug.Log("Defend");
                    combatController.DefendCurrentPlayer();
                    break;
                case 2:
                    Debug.Log("Ability");
                    break;
            }
            
            combatController.PlayerRound = false;
        }
    }
}
