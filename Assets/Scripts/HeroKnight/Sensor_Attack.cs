using UnityEngine;

namespace HeroKnight
{
    public class Sensor_Attack : MonoBehaviour
    {
        private bool       in_range;
        public  Collider2D enemy;

        private float m_DisableTimer;

        // private void OnEnable()
        // {
        //     m_ColCount = 0;
        // }

        public Collider2D State()
        {
            if ((m_DisableTimer > 0) & in_range)
            {
                return enemy;
            }

            return null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemy    = other;
                in_range = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                in_range = false;
            }
        }

        private void Update()
        {
            m_DisableTimer -= Time.deltaTime;
        }

        public void Disable(float duration)
        {
            m_DisableTimer = duration;
        }
    }
}