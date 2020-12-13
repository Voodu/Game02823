using Other;
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
                    LocatePlayer();
                }

                return player;
            }
        }

        private void Awake()
        {
            SceneManager.Instance.OnSceneLoaded((arg0, mode) => LocatePlayer());
        }

        private void LocatePlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight.HeroKnight>();
        }
    }
}