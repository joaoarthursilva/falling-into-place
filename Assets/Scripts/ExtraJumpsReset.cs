using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ExtraJumpsReset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 6) return;
        // col.GetComponent<PlayerCharacterController>().Respawn();
        gameObject.SetActive(false);
    }
}