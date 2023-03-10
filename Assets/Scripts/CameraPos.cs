using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        var position = player.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
