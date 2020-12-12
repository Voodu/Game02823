using System.Collections.Generic;
using Common;
using Inventory_Bak;
using Statistics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Inventory = Statistics.Inventory;

public class InventoryManager : Singleton<InventoryManager>
{
    public           GameObject            inventoryBar;
    public           GameObject            equipmentBar;
    public           GameObject            inventorySlotPrefab;
    [FormerlySerializedAs("inventoryItemPrefab")]
    public           GameObject gearItemPrefab;
    public           GameObject itemPrefab;
    public           HeroKnight.HeroKnight player;
    private          Inventory             inventory;
    private readonly List<ItemSlot>        slots = new List<ItemSlot>();

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
                itemImage.sprite  = item.gameObject.GetComponent<SpriteRenderer>().sprite;
                itemImage.enabled = true;
                slot.Item         = item;
                slot.Occupied     = true;

                if (item is GearItem gear)
                {
                    itemImage.GetComponent<Button>().onClick.AddListener(EquipGearItemUi(gear));
                }

                break;
            }
        }
    }

    public void RemoveInventoryItemUi(GameObject uiSlot)
    {
        var itemSlot  = uiSlot.GetComponent<ItemSlot>();
        var itemImage = itemSlot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
        itemImage.enabled = false;
        itemSlot.Occupied = false;
        itemImage.GetComponent<Button>().onClick.RemoveAllListeners();
        player.characterData.Inventory.RemoveItem(itemSlot.Item);
        SpawnDroppedItem(itemSlot.Item, itemImage);

        if (itemSlot.Item is GearItem g)
        {
            UnequipGearItemUi(g)();
        }
    }

    public void SpawnDroppedItem(Item item, Image itemImage)
    {
        var position = player.transform.position;
        var playerPos = new Vector2(position.x + 1, position.y + 0.5f);
        switch (item)
        {
            case GearItem g:
                var spawnedGearItem = Instantiate(gearItemPrefab, playerPos, Quaternion.identity);
                spawnedGearItem.GetComponent<SpriteRenderer>().sprite = itemImage.sprite;
                var gearItem = spawnedGearItem.GetComponent<GearItem>();
                gearItem.type = g.type;
                gearItem.bonus = g.bonus;
                gearItem.Tier = g.Tier;
                gearItem.Name = g.Name;
                break;
            case Item i:
                var spawnedItem = Instantiate(itemPrefab, playerPos, Quaternion.identity);
                spawnedItem.GetComponent<SpriteRenderer>().sprite = itemImage.sprite;
                var component = spawnedItem.GetComponent<Item>();
                component.Tier  = i.Tier;
                component.Name  = i.Name;
                break;
        }
    }

    private UnityAction EquipGearItemUi(GearItem item)
    {
        return () =>
               {
                   if (player.characterData.EquipItem(item))
                   {
                       var slotName  = item.type + "Slot";
                       var slot      = equipmentBar.transform.Find(slotName);
                       var itemImage = slot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
                       itemImage.sprite  = item.gameObject.GetComponent<SpriteRenderer>().sprite;
                       itemImage.enabled = true;

                       slot.transform.Find("Cross").GetComponent<Button>().onClick.AddListener(UnequipGearItemUi(item));
                   }

                   print(player.characterData.Health.Value);
               };
    }

    private UnityAction UnequipGearItemUi(GearItem item)
    {
        return () =>
               {
                   player.characterData.Unequip(item);
                   var slotName  = item.type + "Slot";
                   var slot      = equipmentBar.transform.Find(slotName);
                   var itemImage = slot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
                   itemImage.enabled = false;

                   slot.transform.Find("Cross").GetComponent<Button>().onClick.RemoveAllListeners();
               };
    }

    private void CreateUiSlots()
    {
        for (var i = 0; i < inventory.Size; i++)
        {
            var go = Instantiate(inventorySlotPrefab, inventoryBar.transform);
            go.transform.Find("Cross").GetComponent<Button>().onClick.AddListener(() => RemoveInventoryItemUi(go));
            var itemSlot = go.GetComponent<ItemSlot>();
            slots.Add(itemSlot);
        }
    }
}