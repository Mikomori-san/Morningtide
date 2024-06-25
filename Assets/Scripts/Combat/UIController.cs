using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject defendButton;
    [SerializeField] private GameObject abilityButton;
    [SerializeField] private TMPro.TextMeshProUGUI hpText;
    [SerializeField] private GameObject hpImage;
    
    [SerializeField] private TMPro.TextMeshProUGUI enemyName;
    [SerializeField] private TMPro.TextMeshProUGUI enemyHealth;
    [SerializeField] private TMPro.TextMeshProUGUI enemyMana;
    [SerializeField] private TMPro.TextMeshProUGUI enemyAttack;
    [SerializeField] private TMPro.TextMeshProUGUI enemyMagic;
    [SerializeField] private TMPro.TextMeshProUGUI enemyDefense;
    [SerializeField] private TMPro.TextMeshProUGUI enemyMDefense;
    

    private EnemyLookAtPointLogic enemyPointer;
    private CombatController combatController;
    private AIBehavior aiBehavior;
    
    int currentButton = 0;
    const int maxButtons = 2;
    
    private bool areActive = false;
    
    public void SetButtonsActive()
    {
        attackButton.SetActive(true);
        defendButton.SetActive(true);
        abilityButton.SetActive(true);
        hpText.gameObject.SetActive(true);
        hpImage.SetActive(true);
        enemyName.gameObject.SetActive(true);
        enemyHealth.gameObject.SetActive(true);
        enemyMana.gameObject.SetActive(true);
        enemyAttack.gameObject.SetActive(true);
        enemyMagic.gameObject.SetActive(true);
        enemyDefense.gameObject.SetActive(true);
        enemyMDefense.gameObject.SetActive(true);
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

        hpText.text = $"{combatController.players[0].GetComponent<BaseStats>().Health} / {combatController.players[0].GetComponent<BaseStats>().MaxHealth}";
        UpdateEnemyStats(combatController.enemies[enemyPointer.GetCurrentEnemyIndex()]);
        
        if(!combatController.PlayerRound) return;

        DoPlayerHud();
    }

    private void DoPlayerHud()
    {
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
            Debug.Log("Enemy Round");
        }
    }

    public void UpdateEnemyStats(GameObject enemy)
    {
        BaseStats enemyStats = enemy.GetComponent<BaseStats>();
        aiBehavior = enemy.GetComponent<AIBehavior>();
        
        enemyName.text = "Bomber " + enemy.name;
        enemyHealth.text = $"HP: {enemyStats.Health} / {enemyStats.MaxHealth}";
        enemyMana.text = $"Mana: {enemyStats.Mana} / {enemyStats.MaxMana}";
        enemyAttack.text = "Attack: " + enemyStats.Attack.ToString();
        enemyMagic.text = "Magic: " + enemyStats.Magic.ToString();
        
        enemyDefense.color = aiBehavior.hasPhysicallyDefended ? Color.green : Color.white;
        enemyMDefense.color = aiBehavior.hasMagicallyDefended ? Color.green : Color.white;

        enemyDefense.text = "Defense: " + enemyStats.Defense.ToString();
        enemyMDefense.text = "M. Defense: " + enemyStats.MagicDefense.ToString();
    }
}
