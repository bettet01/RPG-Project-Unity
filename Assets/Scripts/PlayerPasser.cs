using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPasser : MonoBehaviour
{
    public List<string> equipmentnames;
    public List<Item> itemlist;

    public int playerid;
    public string CharacterName;
    public int coins;
    public int level = 1;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int maxExp = 100;
    public int currentExp;
    public int strength;
    public int defense;
    public int dexterity;
    public int intelligence;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
