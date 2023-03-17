using Player;
using UnityEngine;

namespace Managers
{
    public class NextLevel : MonoBehaviour
    {
        public Transform nextLevel;
        private PlayerCharacterController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerCharacterController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("nextlevel");
            _player.SetSpawnPoint(nextLevel.position);
            _player.Respawn();
        }
    }
}