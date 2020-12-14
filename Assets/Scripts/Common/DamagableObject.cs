using Statistics;
using UnityEngine;
using Weapons;
using System.Collections;

namespace Common
{
    [RequireComponent(typeof(Collider2D))]
    public class DamagableObject : MonoBehaviour
    {
        [SerializeField]
        private int health = 1;

        [SerializeField]
        private int killExperience = 1;

        private Animator animator;
        public float timeToColor = 0.1f;
        SpriteRenderer sr;
        Color defaultColor;
        private Rigidbody2D body2d;
    
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            defaultColor = sr.color;
            animator = GetComponent<Animator>();
            body2d =  GetComponent<Rigidbody2D>();
        }
    
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
                health -= GameManager.Instance.Player.GetStrength();
                if (health <= 0)
                {
                    body2d.velocity = Vector2.zero;
                    animator.SetTrigger("Death");
                }
                else
                {
                    StartCoroutine("SwitchColor");
                }
            }
        }

        public void Death()
        {
            StatisticsManager.Instance.AddExperience(killExperience);
            Destroy(this.gameObject);
        }


        IEnumerator SwitchColor()
        {
            sr.color = new Color(1f, 0.50196078f, 0.50196078f);
            yield return new WaitForSeconds(timeToColor);
            sr.color = defaultColor;
        }
    }
}

