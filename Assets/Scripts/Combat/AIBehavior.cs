using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] private GameObject combatControllerObject;
    
    private CombatController combatController;
    private EnemyLookAtPointLogic enemyPointer;
    private bool isInAnimation = false;
    
    [HideInInspector] public bool isDone = false;
    [HideInInspector] public bool hasDefended = false;
    
    void Start()
    {
        combatController = combatControllerObject.GetComponent<CombatController>();
        enemyPointer = combatControllerObject.GetComponent<EnemyLookAtPointLogic>();
    }

    void Update()
    {
        if (combatController.PlayerRound)
        {
            isDone = false;
            return;
        }
        
        if(combatController.enemyNameHasTurn != int.Parse(name) || isInAnimation) return;
        
        
        enemyPointer.SetCurrentEnemyIndex(combatController.enemyIndexHasTurn);

        if (hasDefended)
        {
            GetComponent<BaseStats>().NerfDefense(5);
            hasDefended = false;
        }
        
        int randomNum = Random.Range(0, combatController.players.Count);
        int randomNum2 = Random.Range(0, 3);

        switch (randomNum2)
        {
            case 0:
            case 1:
                StartCoroutine(AttackPlayer(randomNum));
                Debug.Log("Enemy " + name + " has attacked!");
                break;
            case 2:
                StartCoroutine(Defend());
                Debug.Log("Enemy " + name + " has defended!");
                break;
        }

        isInAnimation = true;
    }

    private IEnumerator Defend()
    {
        GetComponent<BaseStats>().BuffDefense(5);
        hasDefended = true;
        yield return new WaitForSeconds(1);
        
        isDone = true;
        isInAnimation = false;
        combatController.IncreaseEnemyTurn();
    }

    IEnumerator AttackPlayer(int playerIndex)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        Debug.Log(name + " is attacking player, and " + GetComponent<Animator>().GetBool("isAttacking"));
        combatController.AttackTargetedPlayer(playerIndex, GetComponent<BaseStats>().Attack);

        StartCoroutine(ThrowBomb());
        
        yield return new WaitForEndOfFrame();
        
        while (true)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                !GetComponent<Animator>().IsInTransition(0))
            {
                GetComponent<Animator>().SetBool("isAttacking", false);
                isDone = true;
                isInAnimation = false;
                combatController.IncreaseEnemyTurn();
                break;
            }

            yield return null;
        }
    }

    private IEnumerator ThrowBomb()
    {
        float delay = 0.9f;
        yield return new WaitForSeconds(delay);

        GameObject bomb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        bomb.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Vector3 offset = new Vector3(-0.25f, 2, 0);

        bomb.transform.position = transform.position + offset;

        Vector3 playerPosition = combatController.players[0].transform.position;

        Animator animator = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        float animationLength = clipInfo[0].clip.length - delay;
        float journeyLength = Vector3.Distance(transform.position + offset, playerPosition);
        float speed = journeyLength / animationLength;
        float startTime = Time.time;

        Vector3 controlPoint = (transform.position + playerPosition) / 2 + Vector3.up * 5;

        while (bomb.transform.position != playerPosition && Vector3.Distance(bomb.transform.position, playerPosition) > 0.5f)
        {
            float t = (Time.time - startTime) * speed / journeyLength;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            bomb.transform.position = (1 - t) * (1 - t) * (transform.position + offset) + 2 * (1 - t) * t * controlPoint + t * t * playerPosition;

            yield return null;
        }

        Destroy(bomb);
    }
}