using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Other
{
    public class HealthUiManager : Singleton<HealthUiManager>
    {
        private readonly List<GameObject> hearts = new List<GameObject>();
        public           GameObject       heartPrefab;
        public           GameObject       healthBar;

        [SerializeField]
        private int maxHearts = 5;

        public void UpdateHeartsUi(float currentHealth, float maxHealth)
        {
            var heartsLeft = Mathf.Max(0, Mathf.Ceil(currentHealth / maxHealth * maxHearts));

            while (hearts.Count > heartsLeft)
            {
                Destroy(hearts[hearts.Count - 1]);
                hearts.RemoveAt(hearts.Count - 1);
            }

            while (hearts.Count < heartsLeft)
            {
                hearts.Add(Instantiate(heartPrefab, healthBar.transform));
            }
        }
    }
}