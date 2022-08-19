using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public Transform nextLevel;
    public GameObject player;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("nextlevel");
        player.transform.position = nextLevel.position;
    }
}
