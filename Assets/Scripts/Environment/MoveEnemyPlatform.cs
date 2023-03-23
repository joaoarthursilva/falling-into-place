using UnityEngine;

namespace Environment
{
    public class MoveEnemyPlatform : MonoBehaviour
    {
        [SerializeField] private Transform rightTransform;
        [SerializeField] private Transform leftTransform;
        private Vector3 _rightPos;
        private Vector3 _leftPos;
        [SerializeField] private float speed;
        private bool _shouldGoRight;

        private void Start()
        {
            _shouldGoRight = true;
            _rightPos = rightTransform.position;
            _leftPos = leftTransform.position;
            transform.position = _leftPos;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_shouldGoRight)
                MoveRight();
            else
            {
                MoveLeft();
            }
        }

        private void MoveRight()
        {
            var position = transform.position;
            position = Vector2.MoveTowards(new Vector2(position.x, position.y),
                _rightPos, speed * Time.deltaTime);
            transform.position = position;
            if (transform.position == _rightPos)
            {
                _shouldGoRight = false;
            }
        }

        private void MoveLeft()
        {
            var position = transform.position;
            transform.position = Vector2.MoveTowards(new Vector2(position.x, position.y),
                _leftPos, speed * Time.deltaTime);

            if (transform.position == _leftPos)
            {
                _shouldGoRight = true;
            }
        }
    }
}