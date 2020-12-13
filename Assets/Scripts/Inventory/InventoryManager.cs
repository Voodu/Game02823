using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private readonly List<ItemSlot>        slots = new List<ItemSlot>();
        public           GameObject            inventoryBar;
        public           GameObject            equipmentBar;
        public           GameObject            inventorySlotPrefab;
        public           GameObject            gearItemPrefab;
        public           GameObject            itemPrefab;
        private          Inventory             inventory;

        private void Start()
        {
            inventory = GameManager.Instance.Player.characterData.inventory;
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
            GameManager.Instance.Player.characterData.inventory.RemoveItem(itemSlot.Item);
            SpawnDroppedItem(itemSlot.Item, itemImage);

            if (itemSlot.Item is GearItem g)
            {
                UnequipGearItemUi(g)();
            }
        }

        public void SpawnDroppedItem(Item item, Image itemImage)
        {
            var position  = GameManager.Instance.Player.transform.position;
            var playerPos = new Vector2(position.x + 1, position.y + 0.5f);
            switch (item)
            {
                case GearItem g:
                    var spawnedGearItem = Instantiate(gearItemPrefab, playerPos, Quaternion.identity);
                    spawnedGearItem.GetComponent<SpriteRenderer>().sprite = itemImage.sprite;
                    var gearItem = spawnedGearItem.GetComponent<GearItem>();
                    gearItem.type     = g.type;
                    gearItem.bonus    = g.bonus;
                    gearItem.tier     = g.tier;
                    gearItem.itemName = g.itemName;
                    break;
                case Item i:
                    var spawnedItem = Instantiate(itemPrefab, playerPos, Quaternion.identity);
                    spawnedItem.GetComponent<SpriteRenderer>().sprite = itemImage.sprite;
                    var component = spawnedItem.GetComponent<Item>();
                    component.tier     = i.tier;
                    component.itemName = i.itemName;
                    break;
            }
        }

        private UnityAction EquipGearItemUi(GearItem item)
        {
            return () =>
                   {
                       if (GameManager.Instance.Player.characterData.EquipItem(item))
                       {
                           var slotName  = item.type + "Slot";
                           var slot      = equipmentBar.transform.Find(slotName);
                           var itemImage = slot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
                           itemImage.sprite  = item.gameObject.GetComponent<SpriteRenderer>().sprite;
                           itemImage.enabled = true;

                           slot.transform.Find("Cross").GetComponent<Button>().onClick.AddListener(UnequipGearItemUi(item));
                       }
                   };
        }

        private UnityAction UnequipGearItemUi(GearItem item)
        {
            return () =>
                   {
                       GameManager.Instance.Player.characterData.Unequip(item);
                       var slotName  = item.type + "Slot";
                       var slot      = equipmentBar.transform.Find(slotName);
                       var itemImage = slot.gameObject.transform.Find("Slot").transform.Find("Item").GetComponent<Image>();
                       itemImage.enabled = false;

                       slot.transform.Find("Cross").GetComponent<Button>().onClick.RemoveAllListeners();
                   };
        }

        private void CreateUiSlots()
        {
            for (var i = 0; i < inventory.size; i++)
            {
                var go = Instantiate(inventorySlotPrefab, inventoryBar.transform);
                go.transform.Find("Cross").GetComponent<Button>().onClick.AddListener(() => RemoveInventoryItemUi(go));
                var itemSlot = go.GetComponent<ItemSlot>();
                slots.Add(itemSlot);
            }
        }
    }
}