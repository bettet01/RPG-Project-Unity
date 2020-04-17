using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }


    #endregion

    public GameObject player;

    void Start()
    {
        OnLoad();
    }

    public void OnLoad()
    {
        PlayerPasser loadedplayer = FindObjectOfType<PlayerPasser>();
        PlayerStats stats = player.GetComponent<PlayerStats>();
        EquipmentManager equipmanager = player.GetComponent<EquipmentManager>();
        Inventory inventory = player.GetComponent<Inventory>();
        stats.CharacterName = loadedplayer.CharacterName;
        stats.coins = loadedplayer.coins;
        stats.level = loadedplayer.level;
        stats.currentHealth = loadedplayer.currentHealth;
        stats.maxHealth = loadedplayer.maxHealth;
        stats.maxExp = loadedplayer.maxExp;
        stats.currentExp = loadedplayer.currentExp;
        stats.currentMana = loadedplayer.currentMana;
        stats.maxMana = loadedplayer.maxMana;
        stats.defense.SetValue(loadedplayer.defense);
        stats.strength.SetValue(loadedplayer.strength);
        stats.dexterity.SetValue(loadedplayer.dexterity);
        stats.intelligence.SetValue(loadedplayer.intelligence); 

        for(int i = 0; i < loadedplayer.equipmentnames.Count; i++)
        {
            Debug.Log(loadedplayer.equipmentnames[i]);
            Equipment next = (Equipment)Instantiate(Resources.Load(loadedplayer.equipmentnames[i]));
            equipmanager.Equip(next);
        }
        foreach (Item item in loadedplayer.itemlist){
            inventory.Add(item);
        }
    }

    public void OnSave()
    {

    }


    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
