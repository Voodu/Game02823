﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroKnight
{
    public class Sensor_HeroKnight : MonoBehaviour
    {
        private int m_ColCount;

        private float m_DisableTimer;

        private void Awake()
        {
            SceneManager.sceneUnloaded += arg0 => m_ColCount = 0;
        }

        private void OnEnable()
        {
            m_ColCount = 0;
        }

        public bool State()
        {
            if (m_DisableTimer > 0)
            {
                return false;
            }

            return m_ColCount > 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            m_ColCount++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            m_ColCount--;
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