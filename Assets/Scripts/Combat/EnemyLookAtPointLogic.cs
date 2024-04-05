using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyLookAtPointLogic : MonoBehaviour
{
    public Transform enemyLookAtPoint;

    [HideInInspector]
    public List<GameObject> enemies;

    [HideInInspector]
    public Transform currentPlayerTransform;

    int i = 0;

    private bool isStartOfFight = true;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        enemies.Sort((x, y) => GetNumberFromName(x.name).CompareTo(GetNumberFromName(y.name)));

        currentPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        enemyLookAtPoint.transform.position = currentPlayerTransform.position;
        currentPlayerTransform.LookAt(enemyLookAtPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartOfFight)
        {
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                i = i == enemies.Count - 1 ? 0 : i + 1;
            }
            else if(Keyboard.current.aKey.wasPressedThisFrame)
            {
                i = i == 0 ? enemies.Count - 1 : i - 1;
            }

            if(Keyboard.current.wasUpdatedThisFrame)
            {
                enemyLookAtPoint.transform.position = enemies.ElementAt(i).transform.position;
                currentPlayerTransform.LookAt(enemyLookAtPoint.position);
            }
        }
    }

    public void SetStartFalse()
    {
        isStartOfFight = false;
        enemyLookAtPoint.transform.position = enemies.ElementAt(0).transform.position;
    }
    private int GetNumberFromName(string name)
    {
        int number;
        if (int.TryParse(name, out number))
        {
            return number;
        }
        else
        {
            return int.MaxValue;
        }
    }
}
