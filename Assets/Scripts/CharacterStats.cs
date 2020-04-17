using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public String CharacterName;
    public int coins;
    public int level = 1;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int maxExp = 100;
    public int currentExp;
    public Stat strength;
    public Stat defense;
    public Stat dexterity;
    public Stat intelligence;

    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {

        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        Debug.Log("Took " + damage + " Damage");
        currentHealth -= damage;

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);


        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        // To Be Overwritten
        Debug.Log(transform.name + " Died.");
    }
}
