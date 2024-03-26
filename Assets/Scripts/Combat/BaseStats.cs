using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public int MaxHealth = 100 ;

    public int Health;

    public int OriginalAttack = 10;

    public int Attack;

    public int OriginalDefense = 5;

    public int Defense;

    public int OriginalSpeed = 10;

    public int Speed;

    public int OriginalMagic = 10;

    public int Magic;

    public int OriginalMagicDefense = 5;

    public int MagicDefense;

    private void Start()
    {
        Health = Health != 0 ? Health : MaxHealth;
        Attack = Attack != 0 ? Attack : OriginalAttack;
        Defense = Defense != 0 ? Defense : OriginalDefense;
        Speed = Speed != 0 ? Speed : OriginalSpeed;
        Magic = Magic != 0 ? Magic : OriginalMagic;
        MagicDefense = MagicDefense != 0 ? MagicDefense : OriginalMagicDefense;
    }

    public void GetDamage(int damage)
    {
        this.Health -= damage;
    }

    public void Heal(int heal)
    {
        this.Health += heal;
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
        this.Speed += speed;
    }

    public void NerfSpeed(int speed)
    {
        this.Speed -= speed;
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
