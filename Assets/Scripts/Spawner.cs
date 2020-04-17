using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float radius;
    public GameObject enemy;
    public List<GameObject> enemylist;
    public List<GameObject> dropables;
    public Transform spawnTransform;

    public int enemyMaxCount;
    public float respawnTime;
    public float itemChance;
    private  float timeTillNext;

    Vector3 randomLocation;


    // Update is called once per frame
    void Update()
    {
        if(enemylist.Count < enemyMaxCount)
        {
            if(timeTillNext > respawnTime)
            {
                randomLocation = Random.insideUnitSphere * radius;
                randomLocation.y = 0;
                GameObject newEnemy = Instantiate(enemy, transform.position + randomLocation, transform.rotation);
                for(int i =0; i < dropables.Count; i++)
                {
                    int chance = Random.Range(0, 100);
                    if(chance < itemChance * 100)
                    {
                        newEnemy.GetComponent<EnemyStats>().Dropitems.Add(dropables[i]);
                    }
                }
                newEnemy.GetComponent<EnemyStats>().spawnManager = this;
                enemylist.Add(newEnemy);
                timeTillNext = 0;
            }
            timeTillNext += Time.deltaTime;
        }
    }


    private void OnDrawGizmosSelected()
    {

        if (spawnTransform == null)
        {
            spawnTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnTransform.position, radius);
    }
}
