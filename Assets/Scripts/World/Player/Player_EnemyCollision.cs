using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_EnemyCollision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Switch to combat scene when player collides with enemy
        if(other.gameObject.CompareTag("Enemy") && other is CapsuleCollider)
        {
            if(other.name.Contains("1"))
                SceneManager.LoadScene("CombatScene_1Enemy");
            else if(other.name.Contains("2"))
                SceneManager.LoadScene("CombatScene_2Enemies");
            else if(other.name.Contains("3"))
                SceneManager.LoadScene("CombatScene_3Enemies");
            
            print("Switching to combat scene");
        }
        else
        {
            if (other.name.Contains("Healing"))
            {
                PlayerState.Instance.ResetHealthAndMana();
                Destroy(other.gameObject);
            }
        }
    }
}
