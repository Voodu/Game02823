using UnityEngine;

namespace Inventory_Bak
{
    public class Slot : MonoBehaviour
    {

        // Update is called once per frame
        //private void Update()
        //{
        //    if (transform.childCount <= 0)
        //    {
        //        Inventory.isFull[i] = false;
        //    }
        //}

        public void DropItem()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                Destroy(child.gameObject);
            }
        }
    }
}