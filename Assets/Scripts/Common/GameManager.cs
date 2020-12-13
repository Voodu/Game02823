using System;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        public HeroKnight.HeroKnight player;
    }
}