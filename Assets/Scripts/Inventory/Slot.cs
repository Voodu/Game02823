using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update

    private Inventory Inventory;
    public int i; 

    void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 0)
        {
            Inventory.isFull[i] = false; 
        }
    }

    public void DropItem ()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}
