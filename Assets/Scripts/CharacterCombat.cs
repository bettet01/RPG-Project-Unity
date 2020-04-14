using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    private CharacterStats myStats;
    public float attackSpeed = 1f;
    public float attackDelay = 1f;
    private float attackCooldown;
    const float combatCooldown = 5;
    float lastAttackTime;
    public bool inCombat { get; private set; }

    public event System.Action OnAttack;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if(Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }


    public void Attack(CharacterStats targetStats)
    {
        if(targetStats != null)
        {
            if (attackCooldown <= 0f)
            {
                StartCoroutine(DoDamage(targetStats, attackDelay));
                attackCooldown = 1f * attackSpeed;
                lastAttackTime = Time.time;
            }

            inCombat = true;

            if (OnAttack != null)
            {
                OnAttack();
            }
        }

    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.strength.GetValue());

        if(stats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
