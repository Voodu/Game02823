using UnityEngine;

namespace HeroKnight {
    public class Sensor_Attack : MonoBehaviour {

        private bool       in_range = false;
        public  Collider2D enemy;    

        private float m_DisableTimer;

        // private void OnEnable()
        // {
        //     m_ColCount = 0;
        // }

        public Collider2D State()
        {
            if (m_DisableTimer > 0 & in_range == true)
                return enemy;
            return null;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemy    = other;
                in_range = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy"){
                in_range = false;
            }
        }

        void Update()
        {
            m_DisableTimer -= Time.deltaTime;
        }

        public void Disable(float duration)
        {
            m_DisableTimer = duration;
        }
    }
}

