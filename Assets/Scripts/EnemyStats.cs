using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public List<GameObject> Dropitems;
    public float radius;
    public Spawner spawnManager;
    public override void Die()
    {
        base.Die();
        // add ragdoll / death animation
        PlayerManager.instance.player.GetComponent<PlayerStats>().coins += gameObject.GetComponent<CharacterStats>().coins;
        PlayerManager.instance.player.GetComponent<PlayerStats>().addExp(gameObject.GetComponent<CharacterStats>().currentExp);
        Destroy(gameObject);
        spawnManager.enemylist.Remove(gameObject);
        // add loot
        foreach (GameObject pickupItem in Dropitems)
        {
            Instantiate(pickupItem, new Vector3(transform.position.x + Random.Range(-radius,radius), transform.position.y + .2f, transform.position.z + Random.Range(-radius, radius)), Quaternion.identity);
        }
    }
}
 