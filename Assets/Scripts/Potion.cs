using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{

    public string potionType;



    public override void Use()
    {

        base.Use();
        if (potionType.Equals("health"))
        {
            if (PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth < PlayerManager.instance.player.GetComponent<PlayerStats>().maxHealth)
            {
                PlayerManager.instance.player.GetComponent<PlayerStats>().Heal(20);
                RemoveFromInventory();

            }
            else
            {
                Debug.Log("Already at Max Health");
            }
        }


    }
}
