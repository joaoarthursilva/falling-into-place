using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Components")] private Rigidbody2D _rb;
    private Vector3 _position;

    [Header("Layer Masks")] [SerializeField]
    private LayerMask _groundLayer;

    [Header("Movement Variables")] [SerializeField]
    private float _movementAcceleration;

    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _groundLinearDrag;
    private float _horizontalDirection;

    private bool _changingDirection => (_rb.velocity.x > 0f && _horizontalDirection < 0f) ||
                                       (_rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")] [SerializeField]
    private float _jumpForce = 12f;

    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private int _extraJumps = 1;
    [SerializeField] private float _coyoteTime = .1f;
    [SerializeField] private float _jumpBufferLength = .1f;
    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;
    private int _extraJumpsValue;
    private bool _canJump => _jumpBufferCounter > 0f && (_coyoteTimeCounter > 0f || _extraJumpsValue > 0);

    [Header("Ground Collision Variables")] [SerializeField]
    private float _groundRaycastLength;

    [SerializeField] private Vector3 _groundRaycastOffset;
    private bool _onGround;

    [Header("Corner Correction Variables")] [SerializeField]
    private float _topRaycastLength;

    [SerializeField] private Vector3 _edgeRaycastOffset;
    [SerializeField] private Vector3 _innerRaycastOffset;
    private bool _canCornerCorrect;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _position = transform.position;
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferLength;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        _horizontalDirection = GetInput().x;
        if (_canJump) Jump();
    }


    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        if (_onGround)
        {
            _coyoteTimeCounter = _coyoteTime;
            _extraJumpsValue = _extraJumps;
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (_canCornerCorrect) CornerCorrect(_rb.velocity.y);
    }


    private void Jump()
    {
        if (!_onGround) _extraJumpsValue--;
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _jumpBufferCounter = 0f;
    }

    private void FallMultiplier()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);

        if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _groundLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }

    void CornerCorrect(float velocityY)
    {
        //Push player to the right
        RaycastHit2D _hit = Physics2D.Raycast(_position - _innerRaycastOffset + Vector3.up * _topRaycastLength,
            Vector3.left, _topRaycastLength, _groundLayer);
        if (_hit.collider != null)
        {
            float _newPos = Vector3.Distance(
                new Vector3(_hit.point.x, _position.y, 0f) + Vector3.up * _topRaycastLength,
                _position - _edgeRaycastOffset + Vector3.up * _topRaycastLength);
            _position = new Vector3(_position.x + _newPos, _position.y, _position.z);
            _rb.velocity = new Vector2(_rb.velocity.x, velocityY);
            return;
        }

        //Push player to the left
        _hit = Physics2D.Raycast(_position + _innerRaycastOffset + Vector3.up * _topRaycastLength,
            Vector3.right, _topRaycastLength, _groundLayer);
        if (_hit.collider != null)
        {
            float _newPos = Vector3.Distance(
                new Vector3(_hit.point.x, _position.y, 0f) + Vector3.up * _topRaycastLength,
                _position + _edgeRaycastOffset + Vector3.up * _topRaycastLength);
            _position =
                new Vector3(_position.x - _newPos, _position.y, _position.z);
            _rb.velocity = new Vector2(_rb.velocity.x, velocityY);
        }
    }

    private void CheckCollisions()
    {
        //Ground
        _onGround = Physics2D.Raycast(_position + _groundRaycastOffset, Vector2.down, _groundRaycastLength,
                        _groundLayer) ||
                    Physics2D.Raycast(_position - _groundRaycastOffset, Vector2.down, _groundRaycastLength,
                        _groundLayer);

        //Corner
        _canCornerCorrect =
            Physics2D.Raycast(_position + _edgeRaycastOffset, Vector2.up, _topRaycastLength, _groundLayer) &&
            !Physics2D.Raycast(_position + _innerRaycastOffset, Vector2.up, _topRaycastLength,
                _groundLayer) ||
            Physics2D.Raycast(_position - _edgeRaycastOffset, Vector2.up, _topRaycastLength, _groundLayer) &&
            !Physics2D.Raycast(_position - _innerRaycastOffset, Vector2.up, _topRaycastLength, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Ground check
        Gizmos.DrawLine(_position + _groundRaycastOffset,
            _position + _groundRaycastOffset + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(_position - _groundRaycastOffset,
            _position - _groundRaycastOffset + Vector3.down * _groundRaycastLength);

        //Corner Check
        Gizmos.DrawLine(_position + _edgeRaycastOffset,
            _position + _edgeRaycastOffset + Vector3.up * _topRaycastLength);
        Gizmos.DrawLine(_position - _edgeRaycastOffset,
            _position - _edgeRaycastOffset + Vector3.up * _topRaycastLength);
        Gizmos.DrawLine(_position + _innerRaycastOffset,
            _position + _innerRaycastOffset + Vector3.up * _topRaycastLength);
        Gizmos.DrawLine(_position - _innerRaycastOffset,
            _position - _innerRaycastOffset + Vector3.up * _topRaycastLength);

        //Corner Distance Check
        Gizmos.DrawLine(_position - _innerRaycastOffset + Vector3.up * _topRaycastLength,
            _position - _innerRaycastOffset + Vector3.up * _topRaycastLength +
            Vector3.left * _topRaycastLength);
        Gizmos.DrawLine(_position + _innerRaycastOffset + Vector3.up * _topRaycastLength,
            _position + _innerRaycastOffset + Vector3.up * _topRaycastLength +
            Vector3.right * _topRaycastLength);
    }
}