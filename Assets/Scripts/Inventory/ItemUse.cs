using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    private GameObject player;
    //private Controller playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //playerController = player.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use()
    {
        Destroy(gameObject);
        //playerController.TopScore += 20;
        //playerController.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 1500f);
    }
}
