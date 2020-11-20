using Statistics;
using UnityEngine;
using Weapons;

namespace Common {
    [RequireComponent(typeof(Collider2D))]
    public class DamagableObject : MonoBehaviour
    {
        [SerializeField]
        private int health = 1;

        [SerializeField]
        private int killExperience = 1;

        private void OnCollisionEnter2D(Collision2D other)
        {
            GetDamage(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GetDamage(other.gameObject);
        }

        private void GetDamage(GameObject other)
        {
            var weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                health -= weapon.GetDamage();
                if (health <= 0)
                {
                    StatisticsManager.Instance.AddExperience(killExperience);
                    Destroy(gameObject);
                }
            }
        }
    }
}