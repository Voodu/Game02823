﻿using UnityEngine;

namespace Quests {
    [RequireComponent(typeof(ObjectiveItem))]
    [RequireComponent(typeof(Collider2D))]
    public class GatherObjectiveActivator : MonoBehaviour
    {
        private bool activated = false;
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
            if (other.CompareTag("Player") && !activated)
            {
                activated = true;
                GetComponent<ObjectiveItem>().Activate();
            }
        }
    }
}
