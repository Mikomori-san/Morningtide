using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    public int Health { get; set; }
    public int Mana { get; set; }

    public void ResetHealthAndMana()
    {
        Health = _maxHealth;
        Mana = _maxMana;
    }
    
    private int _maxHealth = 100;
    private int _maxMana = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
