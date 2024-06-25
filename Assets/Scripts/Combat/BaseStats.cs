using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseStats : MonoBehaviour
{
    public int MaxHealth = 100 ;

    public int Health;

    public int OriginalAttack = 10;

    public int Attack;

    public int OriginalDefense = 5;

    public int Defense;

    public int MaxMana = 10;

    public int Mana;

    public int OriginalMagic = 10;

    public int Magic;

    public int OriginalMagicDefense = 5;

    public int MagicDefense;
    private void Start()
    {
        if (gameObject.CompareTag("Player") && PlayerState.Instance != null)
        {
            Health = PlayerState.Instance.Health;
            Mana = PlayerState.Instance.Mana;
        }
        
        if(Health == 0)
        {
            Health = MaxHealth;
            Mana = MaxMana;
        }

        Attack = Attack != 0 ? Attack : OriginalAttack;
        Defense = Defense != 0 ? Defense : OriginalDefense;
        Magic = Magic != 0 ? Magic : OriginalMagic;
        MagicDefense = MagicDefense != 0 ? MagicDefense : OriginalMagicDefense;
    }
    public void GetPhysicalDamage(int damage)
    {
        damage = damage / Defense;
        this.Health -= damage;
    }

    public void GetMagicDamage(int damage)
    {
        damage = damage / MagicDefense;
        this.Health -= damage;
    }

    public void Heal(int heal)
    {
        this.Health += heal;
        
        if (this.Health > MaxHealth)
        {
            this.Health = MaxHealth;
        }
    }

    public void RemoveMana(int mana)
    {
        this.Mana -= mana;
    }
    
    public void BuffDefense(int defense)
    {
        this.Defense += defense;
    }

    public void NerfDefense(int defense)
    {
        this.Defense -= defense;
    }

    public void BuffAttack(int attack)
    {
        this.Attack += attack;
    }

    public void NerfAttack(int attack)
    {
        this.Attack -= attack;
    }

    public void BuffSpeed(int speed)
    {
        this.Mana += speed;
    }

    public void NerfSpeed(int speed)
    {
        this.Mana -= speed;
    }

    public void BuffMagic(int magic)
    {
        this.Magic += magic;
    }

    public void NerfMagic(int magic)
    {
        this.Magic -= magic;
    }

    public void BuffMagicDefense(int magicDefense)
    {
        this.MagicDefense += magicDefense;
    }

    public void NerfMagicDefense(int magicDefense)
    {
        this.MagicDefense -= magicDefense;
    }
}
