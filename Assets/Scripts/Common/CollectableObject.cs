using Inventory;
using Statistics;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Collider2D))]
    public class CollectableObject : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Collect(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Collect(other.gameObject);
        }

        private void Collect(GameObject other)
        {
            var heroKnight = other.GetComponent<HeroKnight.HeroKnight>();
            if (heroKnight != null)
            {
                var item = gameObject.GetComponent<Item>();
                if (item != null)
                {
                    if (heroKnight.characterData.inventory.AddItem(item))
                    {
                        InventoryManager.Instance.AddInventoryItemUi(item);
                        gameObject.SetActive(false);
                    }
                }

                Destroy(gameObject);
            }
        }
    }
}