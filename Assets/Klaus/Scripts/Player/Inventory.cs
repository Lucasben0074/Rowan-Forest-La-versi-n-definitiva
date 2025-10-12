using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;

    private List<InventorySlot> container = new();
    public List<InventorySlot> Container => container;

    private void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;

        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == item)
            {
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            container.Add(new InventorySlot(item, amount));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ItemObject item = other.GetComponent<ItemHolder>().item;
            AddItem(item, 1);
            Destroy(other.gameObject);
        }
    }
}

public class InventorySlot
{
    public ItemObject item;
    public int amount; 

    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}

