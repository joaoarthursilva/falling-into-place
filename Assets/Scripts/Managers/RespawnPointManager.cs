using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Managers
{
    public class RespawnPointManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> respawnList;
        private PlayerCharacterController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerCharacterController>();
        }

        public void UpdateSpawnPoint(int level)
        {
            var pos = respawnList[level]; 
            _player.SetSpawnPoint(pos.position);
        }
    }
}