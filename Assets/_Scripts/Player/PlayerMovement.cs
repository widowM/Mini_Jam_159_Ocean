using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Fields
    [SerializeField] private Player _player;
    [SerializeField] private float _movementSpeed = 7.0f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundCheckLayerMask;
    private bool _isGrounded;

    private bool _isPushingLeftBounds = false;

    // Properties
    public bool IsPushingLeftBounds => _isPushingLeftBounds;

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        _isGrounded = CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb2D.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void MovePlayer()
    { 
        if (IsAtLeftEdgeOfScreen() && _player.PlayerInput.HorizontalInput < 0)
        {
            // Prevent leftward force (?)
            _isPushingLeftBounds = true;

            // My idea was to stop player movement when pushing left bounds
            return;
        }
        else
        {
            _isPushingLeftBounds = false;
        }

        // Walk left-right default only if grounded or player touching rock
        if (_isGrounded || _player.PlayerPhysics.IsTouchingRock)
        {
            Vector2 force = new Vector2(_player.PlayerInput.HorizontalInput * _movementSpeed, 0f);
            _rb2D.AddForce(force);
        }
        else
        {
            // Apply horizontal movement with reduced movement speed without affecting vertical velocity
            Vector2 horizontalVelocity = new Vector2(_player.PlayerInput.HorizontalInput * _movementSpeed * 0.3f, _rb2D.velocity.y);
            _rb2D.velocity = horizontalVelocity;
        }

        if (_player.PlayerInput.HorizontalInput == 0)
        {
            StopPlayer();
        }
    }

    private void StopPlayer()
    {
        _rb2D.velocity = new Vector2(0, _rb2D.velocity.y);
    }

    private bool CheckGrounded()
    {
        Ray ray = new Ray(_groundCheck.position, Vector3.down);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.1f, _groundCheckLayerMask);

        return hit.collider != null;
    }

    // TODO: Check if the player is at the left edge of the screen and trying to move left
    // (Still Buggy)
    private bool IsAtLeftEdgeOfScreen()
    {  
        return Camera.main.WorldToViewportPoint(transform.position).x <= 0.1f;
    }
}
