using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Potion")]
public class PotionObject : ItemObject
{
    public int healthRestoreValue;

    public void Awake()
    {
        type = ItemType.Potion;
    }
}
