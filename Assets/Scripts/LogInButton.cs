using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogInButton : MonoBehaviour
{
    public PlayerPasser playerPasser;
    public TextMeshProUGUI email;
    public TMP_InputField password;
    public TextMeshProUGUI errorBox;
    public List<string> character;
    public bool status;


    private string API = "http://localhost:8080/api/users/player";
    public void ValidateUser()
    {
         StartCoroutine(GetRequest(API));

    }

    IEnumerator GetRequest(string uri)
    {
        character.RemoveRange(0, character.Count);
        errorBox.text = "";
        uri += "?email=" + email.text + "&password=" + password.text;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {

            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError)
            {
                errorBox.text = "Incorrect Email or Password";
                yield return false;
            }
            Debug.Log(webRequest.downloadHandler.text);
            string[] pieces = webRequest.downloadHandler.text.Split(',');

            for (int i = 0; i < 14; i++)
            {
                string[] temp = pieces[i].Split(':');
                character.Add(temp[1]);
            }

            for(int i = 14; i < pieces.Length; i++)
            {
                if (pieces[i].Contains("equipmentname"))
                {
                    string[] temp = pieces[i].Split(':');
                    try
                    {
                        Equipment next = (Equipment)Instantiate(Resources.Load(temp[1].Substring(1, temp[1].Length - 3)));
                        playerPasser.equipmentlist.Add(next);
                    }
                    catch (ArgumentException)
                    {
                        Equipment next = (Equipment)Instantiate(Resources.Load(temp[1].Substring(1, temp[1].Length - 4)));
                        playerPasser.equipmentlist.Add(next);
                    }

                }
                if (pieces[i].Contains("itemname"))
                {
                    string[] temp3 = pieces[i].Split(':');
                    string[] temp2 = pieces[i + 1].Split(':');

                    try
                    {
                        Debug.Log(temp3[0] + " " + temp3[1]);
                        Item next = (Item)Instantiate(Resources.Load(temp3[1].Replace("\"", "")));
                        next.itemCount = int.Parse(temp2[1].Substring(0, temp2[1].Length - 1));
                        playerPasser.itemlist.Add(next);
                    }
                catch (System.Exception)
                    {
                        Debug.Log(temp2[1].Substring(0, temp2[1].Length - 2));
                        Item next = (Item)Instantiate(Resources.Load(temp3[1].Replace("\"", "")));
                        next.itemCount = int.Parse(temp2[1].Substring(0, temp2[1].Length - 3));
                        playerPasser.itemlist.Add(next);
                    }
                }
            }


            playerPasser.playerid = int.Parse(character[0]); 
            playerPasser.CharacterName = character[1].Substring(1,character[1].Length-2);
            playerPasser.coins =  int.Parse(character[2]);
            playerPasser.level = int.Parse(character[3]);
            playerPasser.maxHealth = int.Parse(character[4]);
            playerPasser.currentHealth = int.Parse(character[5]);
            playerPasser.maxMana = int.Parse(character[6]);
            playerPasser.currentMana = int.Parse(character[7]);
            playerPasser.maxExp = int.Parse(character[8]);
            playerPasser.currentExp = int.Parse(character[9]);
            playerPasser.strength = int.Parse(character[10]);
            playerPasser.defense = int.Parse(character[11]);
            playerPasser.dexterity = int.Parse(character[12]);
            playerPasser.intelligence = int.Parse(character[13]);

            webRequest.Dispose();
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
}
