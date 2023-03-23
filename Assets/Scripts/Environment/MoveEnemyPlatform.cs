using System;
using UnityEngine;

namespace Environment
{
    public class MoveEnemyPlatform : MonoBehaviour
    {
        [SerializeField] private bool canMove;
        [SerializeField] private Vector3 rightPos;
        [SerializeField] private Vector3 leftPos;
        private bool isGoingRight;

        private void Start()
        {
        }

        private void Update()
        {
        }

        private void Move()
        {
            if (isGoingRight)
            {
            }

            if (Math.Abs(transform.position.x - rightPos.x) < .1)
            {
            }
        }

        private void Arrived()
        {
        }
    }
}