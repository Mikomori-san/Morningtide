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
    [SerializeField] private GameObject manaImage;
    [SerializeField] private TMPro.TextMeshProUGUI manaText;
    
    [SerializeField] private TMPro.TextMeshProUGUI enemyName;
    [SerializeField] private TMPro.TextMeshProUGUI enemyHealth;
    [SerializeField] private TMPro.TextMeshProUGUI enemyMana;
    [SerializeField] private TMPro.TextMeshProUGUI enemyAttack;
    [SerializeField] private TMPro.TextMeshProUGUI enemyMagic;
    [SerializeField] private TMPro.TextMeshProUGUI enemyDefense;
    [SerializeField] private TMPro.TextMeshProUGUI enemyMDefense;
    
    [SerializeField] private GameObject healButton;
    [SerializeField] private GameObject magicMissileButton;
    [SerializeField] private GameObject backButton;

    private EnemyLookAtPointLogic enemyPointer;
    private CombatController combatController;
    private AIBehavior aiBehavior;
    private BaseStats playerStats;
    
    int currentButton = 0;
    const int maxButtons = 2;
    private const int maxAbilityButtons = 2;
    
    private bool areActive = false;
    private bool abilityButtonsActive = false;
    
    public void SetButtonsActive()
    {
        attackButton.SetActive(true);
        defendButton.SetActive(true);
        abilityButton.SetActive(true);
        
        hpText.gameObject.SetActive(true);
        hpImage.SetActive(true);
        manaText.gameObject.SetActive(true);
        manaImage.SetActive(true);
        
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

        playerStats = combatController.players[0].GetComponent<BaseStats>(); // Late initialization
        
        hpText.text = $"{playerStats.Health} / {playerStats.MaxHealth}";
        manaText.text = $"{playerStats.Mana} / {playerStats.MaxMana}";

        UpdateEnemyStats(combatController.enemies[enemyPointer.GetCurrentEnemyIndex()]);
        
        if(!combatController.PlayerRound) return;

        DoPlayerHud();
    }

    private void DoPlayerHud()
    {
        if (!abilityButtonsActive)
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
                        combatController.PlayerRound = false;
                        Debug.Log("Enemy Round");
                        break;
                    case 1:
                        Debug.Log("Defend");
                        combatController.DefendCurrentPlayer();
                        combatController.PlayerRound = false;
                        Debug.Log("Enemy Round");
                        break;
                    case 2:
                        Debug.Log("Ability");
                        healButton.SetActive(true);
                        magicMissileButton.SetActive(true);
                        backButton.SetActive(true);
                        abilityButtonsActive = true;
                        currentButton = 1;
                        break;
                }
            }
        }
        else
        {
            magicMissileButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = playerStats.Mana >= 20 ? Color.white : Color.red;
            healButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = playerStats.Mana >= 30 ? Color.white : Color.red;
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentButton = currentButton == maxAbilityButtons ? 0 : currentButton + 1;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                currentButton = currentButton == 0 ? maxAbilityButtons : currentButton - 1;
            }
            
            switch (currentButton)
            {
                case 0:
                    healButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    magicMissileButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                    backButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                    break;
                case 1:
                    healButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                    magicMissileButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    backButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                    break;
                case 2:
                    healButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                    magicMissileButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                    backButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    break;
            }
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                switch (currentButton)
                {
                    case 0:
                        Debug.Log("Heal");
                        
                        if (playerStats.Mana >= 30)
                        {
                            combatController.HealPlayer();
                            combatController.PlayerRound = false;
                            
                            Debug.Log("Enemy Round");
                            
                            abilityButtonsActive = false;
                            currentButton = maxButtons;
                            healButton.SetActive(false);
                            magicMissileButton.SetActive(false);
                            backButton.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Not enough mana");
                        }
                        
                        break;
                    case 1:
                        Debug.Log("Magic Missile Attack");
                        
                        if (combatController.players[0].GetComponent<BaseStats>().Mana >= 20)
                        {
                            combatController.MagicMissile();
                            combatController.PlayerRound = false;
                            
                            Debug.Log("Enemy Round");
                            
                            abilityButtonsActive = false;
                            currentButton = maxButtons;
                            healButton.SetActive(false);
                            magicMissileButton.SetActive(false);
                            backButton.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Not enough mana");
                        }
                        
                        break;
                    case 2:
                        Debug.Log("Back");
                        
                        abilityButtonsActive = false;
                        currentButton = maxButtons;
                        healButton.SetActive(false);
                        magicMissileButton.SetActive(false);
                        backButton.SetActive(false);
                        
                        break;
                }
            }
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
