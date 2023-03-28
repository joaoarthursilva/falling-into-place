using UnityEngine;

namespace Player
{
    public class PlayerCharacterController : MonoBehaviour
    {
        [Header("Components")] private Rigidbody2D _rb;
        private Vector3 _position;

        [Header("Layer Masks")] [SerializeField]
        private LayerMask groundLayer;

        [Header("Movement Variables")] [SerializeField]
        private float movementAcceleration = 75;

        [SerializeField] private float maxMoveSpeed = 8f;
        [SerializeField] private float groundLinearDrag = 10f;
        private float _horizontalDirection;

        private bool changingDirection => (_rb.velocity.x > 0f && _horizontalDirection < 0f) ||
                                          (_rb.velocity.x < 0f && _horizontalDirection > 0f);

        [Header("Jump Variables")] [SerializeField]
        private float jumpForce = 25f;

        private bool _jumpInput;

        [SerializeField] private float fallMultiplier = 8f;
        [SerializeField] private float airLinearDrag = 2.5f;
        [SerializeField] private int extraJumps;
        [SerializeField] private float coyoteTime = .1f;
        [SerializeField] private float jumpBufferLength = .1f;
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private int _extraJumpsValue;
        private bool canJump => _jumpBufferCounter > 0f && (_coyoteTimeCounter > 0f || _extraJumpsValue > 0);

        [Header("Ground Collision Variables")] [SerializeField]
        private float groundRaycastLength = 1.37f;

        [SerializeField] private Vector3 groundRaycastOffset;
        private bool _onGround;
        private Vector3 _currentSpawnPoint;

        private void Start()
        {
            _jumpInput = false;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _position = transform.position;
            if (Input.GetButtonDown("Jump") || _jumpInput)
            {
                _jumpBufferCounter = jumpBufferLength;
            }
            else
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            _horizontalDirection = GetInput().x;
            if (canJump) Jump();
        }

        public void InputJump()
        {
            _jumpInput = true;
        }

        private void FixedUpdate()
        {
            CheckCollisions();
            MoveCharacter();
            if (_onGround)
            {
                _coyoteTimeCounter = coyoteTime;
                _extraJumpsValue = extraJumps;
                ApplyGroundLinearDrag();
            }
            else
            {
                ApplyAirLinearDrag();
                FallMultiplier();
                _coyoteTimeCounter -= Time.deltaTime;
            }
        }


        private void Jump()
        {
            if (!_onGround) _extraJumpsValue--;
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpBufferCounter = 0f;
            _jumpInput = false;
        }

        private void FallMultiplier()
        {
            if (_rb.velocity.y != 0)
                _rb.gravityScale = fallMultiplier;
            else
                _rb.gravityScale = 1f;
        }

        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private void MoveCharacter()
        {
            _rb.AddForce(new Vector2(_horizontalDirection, 0f) * movementAcceleration);

            if (Mathf.Abs(_rb.velocity.x) > maxMoveSpeed)
                _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxMoveSpeed, _rb.velocity.y);
        }

        private void ApplyGroundLinearDrag()
        {
            if (Mathf.Abs(_horizontalDirection) < 0.4f || changingDirection)
            {
                _rb.drag = groundLinearDrag;
            }
            else
            {
                _rb.drag = 0f;
            }
        }

        private void ApplyAirLinearDrag()
        {
            _rb.drag = airLinearDrag;
        }

        private void CheckCollisions()
        {
            //Ground
            _onGround = Physics2D.Raycast(_position + groundRaycastOffset, Vector2.down, groundRaycastLength,
                            groundLayer) ||
                        Physics2D.Raycast(_position - groundRaycastOffset, Vector2.down, groundRaycastLength,
                            groundLayer);
        }

        public void SetSpawnPoint(Vector3 point)
        {
            _currentSpawnPoint = point;
        }

        public void Respawn()
        {
            gameObject.transform.position = _currentSpawnPoint;
            _rb.velocity = Vector2.zero;
        }

        public void ResetExtraJumps()
        {
            _extraJumpsValue = extraJumps;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            //Ground check
            Gizmos.DrawLine(_position + groundRaycastOffset,
                _position + groundRaycastOffset + Vector3.down * groundRaycastLength);
            Gizmos.DrawLine(_position - groundRaycastOffset,
                _position - groundRaycastOffset + Vector3.down * groundRaycastLength);


            //flip

            //  if (movementAcceleration < 0)
            //   {
            //       Sprite.flipX = true;
            //    }
            //     else if (movementAcceleration < 0)
            //   {
            //         Sprite.flipX = false;
            //   }
        }
    }
}