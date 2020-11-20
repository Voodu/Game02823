using UnityEngine;

namespace Weapons {
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private int damage = 1;

        public int GetDamage()
        {
            return damage;
        }
    }
}