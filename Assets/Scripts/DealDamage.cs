using UnityEngine;
using Managers;
using Player;

public class DealDamage : MonoBehaviour
{
    [SerializeField]
    private int level;
    // public Transform respawnPoint;
    // private GameObject _player;
    // public float startingHealth = 10f;
    // private float _currentHealth;
    // private Vector3 _respawn;
    private void Start()
    {
        // _player = GameObject.FindGameObjectWithTag("Player");
        // _currentHealth = startingHealth;
        // _respawn = respawnPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 6) return;
        
        col.gameObject.GetComponent<PlayerCharacterController>().Respawn();
        // Debug.Log(_currentHealth);
        // if (_currentHealth > 0)
        // {
        // _currentHealth -= 1;
        // }
        // else
        // {
        // _currentHealth = startingHealth;
        // _player.transform.position = respawnPoint.position;
        // }
    }
}