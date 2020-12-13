using System;
using UnityEngine;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        private HeroKnight.HeroKnight player;

        public HeroKnight.HeroKnight Player
        {
            get
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight.HeroKnight>();
                }

                return player;
            }
        }
    }
}