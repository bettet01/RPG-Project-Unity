using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
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

    public float saveTime;
    private float timetillsave = 0;

    void Start()
    {
        OnLoad();
    }

    private void Update()
    {
        if(saveTime < timetillsave)
        {
            OnSave();
            timetillsave = 0;
        } else
        {
            timetillsave += Time.deltaTime;
        }
    }

    public void OnLoad()
    {
        PlayerPasser loadedplayer = FindObjectOfType<PlayerPasser>();
        PlayerStats stats = player.GetComponent<PlayerStats>();
        EquipmentManager equipmanager = player.GetComponent<EquipmentManager>();
        Inventory inventory = player.GetComponent<Inventory>();
        PlayerAnimator animator = player.GetComponent<PlayerAnimator>();
        stats.id = loadedplayer.playerid;
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

        for(int i = 0; i < loadedplayer.equipmentlist.Count; i++)
        {
            equipmanager.Equip(loadedplayer.equipmentlist[i]);
            animator.OnEquipmentChanged(loadedplayer.equipmentlist[i], null);

        }
        foreach (Item item in loadedplayer.itemlist){
            inventory.Add(item);
        }
    }

    public void OnSave()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        EquipmentManager equipmanager = player.GetComponent<EquipmentManager>();
        Inventory inventory = player.GetComponent<Inventory>();
        string saveData = "{\"id\": " + stats.id + ", " +
            "\"charactername\": \"" + stats.CharacterName + "\", " +
            "\"coins\": " + stats.coins + ", " +
            "\"playerlevel\": " + stats.level + ", " +
            "\"maxhealth\": " + stats.maxHealth + ", " +
            "\"currenthealth\": " + stats.currentHealth + ", " +
            "\"maxmana\": " + stats.maxMana + ", " +
            "\"currentmana\": " + stats.currentMana + ", " +
            "\"maxexp\": " + stats.maxExp + ", " +
            "\"currentexp\": " + stats.currentExp + ", " +
            "\"strength\": " + stats.strength.GetBaseValue() + ", " +
            "\"defense\": " + stats.defense.GetBaseValue() + ", " +
            "\"dexterity\": " + stats.dexterity.GetBaseValue() + ", " +
            "\"intelligence\": " + stats.intelligence.GetBaseValue() + ", " +
            "\"equipmentitems\": [ ";

        foreach(Equipment equipment in equipmanager.currentEquipment)
        { 
            if(equipment == null)
            {

            } else
            {
                if (!equipment.isDefaultItem)
                {
                    saveData += "{ \"equipmentname\": \"" + equipment.name + "\" },";
                }
            }
        }

        saveData = saveData.Substring(0, saveData.Length - 1);
        saveData += "], \"inventoryitems\": [";

        foreach(Item item in inventory.items)
        {
            if(item == null)
            {

            } else
            {
                saveData += "{ \"itemname\": \"" + item.name + "\", " + "\"itemcount\": " + item.itemCount + "},";
            }
        }
        saveData = saveData.Substring(0, saveData.Length - 1);
        saveData += "]}";

        Debug.Log(saveData);
        
        StartCoroutine(SaveData(saveData));


    }


    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator SaveData(string data)
    {
        UnityWebRequest www = UnityWebRequest.Put("http://localhost:8080/api/player", data);
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

    }
}
