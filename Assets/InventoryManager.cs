using System.Collections.Generic;
using Common;
using Statistics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject inventoryBar;
    public GameObject inventorySlotPrefab;
    public HeroKnight.HeroKnight player;
    private Inventory inventory;
    private List<ItemSlot> slots = new List<ItemSlot>();

    private void Start()
    {
        inventory = player.characterData.Inventory;
        CreateUiSlots();
    }

    public void AddInventoryItemUi(Item item)
    {
        foreach (var slot in slots)
        {
            if (!slot.Occupied)
            {
                var itemImage = slot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
                itemImage.sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;
                itemImage.enabled = true;
                slot.Item = item;
                slot.Occupied = true;

                if (item is GearItem gear)
                {
                    itemImage.GetComponent<Button>().onClick.AddListener(() => player.characterData.EquipItem(gear));
                }
                break;
            }
        }
    }

    public void RemoveInventoryItem(GameObject uiSlot)
    {
        var itemSlot = uiSlot.GetComponent<ItemSlot>();
        var itemImage = itemSlot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
        itemImage.enabled = false;
        itemSlot.Occupied = false;

        player.characterData.Inventory.RemoveItem(itemSlot.Item);
    }

    private void CreateUiSlots()
    {
        for (var i = 0; i < inventory.Size; i++)
        {
            var go = Instantiate(inventorySlotPrefab, inventoryBar.transform);
            go.transform.Find("Cross").GetComponent<Button>().onClick.AddListener(() => RemoveInventoryItem(go));
            var itemSlot = go.GetComponent<ItemSlot>();
            slots.Add(itemSlot);
        }
    }
}
