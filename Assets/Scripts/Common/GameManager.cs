using System.Linq;
using Cinemachine;
using Other;
using UnityEngine;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        private bool       ready;
        public  GameObject playerPrefab;

        private HeroKnight.HeroKnight player;

        public string targetSpawn;

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

        private void Start()
        {
            if (!ready)
            {
                SceneManager.Instance.OnSceneLoaded((arg0, mode) => LocatePlayer());
                ready = true;
            }
        }

        private GameObject FindSpawnPoint()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint.GetComponent<SpawnPoint>().spawnName == targetSpawn)
                {
                    return spawnPoint;
                }
            }

            return spawnPoints.First();
        }

        private void SpawnPlayer()
        {
            var spawnPoint = FindSpawnPoint();
            var go         = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
            player = go.GetComponent<HeroKnight.HeroKnight>();
            FollowWithVCam(go);
        }

        private void FollowWithVCam(GameObject go)
        {
            var cam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
            cam.Follow = go.transform;
        }

        private void LocatePlayer()
        {
            var temp = GameObject.FindGameObjectWithTag("Player");
            if (temp == null)
            {
                SpawnPlayer();
            }
            else
            {
                player = temp.GetComponent<HeroKnight.HeroKnight>();
                MovePlayerToSpawn(FindSpawnPoint());
                FollowWithVCam(temp);
            }
        }

        private void MovePlayerToSpawn(GameObject spawnPoint)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}