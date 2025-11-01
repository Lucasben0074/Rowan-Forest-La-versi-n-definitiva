using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;

    private const int xStart = -201;
    private const int yStart = 125;

    private const int xItemOffset = 50;
    private const int yItemOffset = 50;
    private const int columnNumber = 10;

    private Dictionary<InventorySlot, GameObject> displayedItems = new();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();

        //CreateDisplay();
    }

    private void Update()
    {
        //UpdateDisplay();
    }

    //private void CreateDisplay()
    //{
    //    for (int i = 0; i < inventory.Container.Count; i++)
    //    {
    //        GameObject instance = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
    //        instance.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //        instance.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    //        displayedItems.Add(inventory.Container[i], instance);

    //    }
    //}

    //private void UpdateDisplay()
    //{
    //    for (int i = 0; i < inventory.Container.Count; i++)
    //    {
    //        if (displayedItems.ContainsKey(inventory.Container[i]))
    //        {
    //            displayedItems[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    //        }
    //        else
    //        {
    //            var instance = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
    //            instance.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //            instance.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    //            displayedItems.Add(inventory.Container[i], instance);

    //        }
    //    }
    //}

    private Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xItemOffset * (i % columnNumber)), yStart + (-yItemOffset * (i / columnNumber)), 0f);
    }
}
 