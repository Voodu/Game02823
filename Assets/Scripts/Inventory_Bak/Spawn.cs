﻿using UnityEngine;

namespace Inventory_Bak
{
    public class Spawn : MonoBehaviour
    {
        public  GameObject item;
        private Transform  player;

        // Start is called before the first frame update
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        private void Update() { }

        public void SpawnDroppedItem()
        {
            var playerPos = new Vector2(player.position.x + 1, player.position.y + 1);
            Instantiate(item, playerPos, Quaternion.identity);
        }
    }
}