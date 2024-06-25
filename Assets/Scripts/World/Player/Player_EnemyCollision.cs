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
            print("Switching to combat scene");
            SceneManager.LoadScene("CombatScene");
        }
    }
}
