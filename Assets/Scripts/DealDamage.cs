using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DealDamage : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public float startingHealth = 10f;
    private float _currentHealth;
    // private Vector3 _respawn;
    private void Start()
    {
        _currentHealth = startingHealth;
        // _respawn = respawnPoint.position;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(_currentHealth);
        if (_currentHealth > 0)
        {
            _currentHealth -= 1;
        }
        else
        {
            _currentHealth = startingHealth;
            player.transform.position = respawnPoint.position;
        }
    }
}