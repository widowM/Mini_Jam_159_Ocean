using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Fields
    [SerializeField] private Player _player;
    [SerializeField] private float _movementSpeed = 7.0f;
    [SerializeField] private Rigidbody2D _rb2D;

    
    private bool _isPushingLeftBounds = false;

    // Properties
    public bool IsPushingLeftBounds => _isPushingLeftBounds;

    private void FixedUpdate()
    {
        MovePlayer();
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

        Vector2 force = new Vector2(_player.PlayerInput.HorizontalInput * _movementSpeed, 0f);
        _rb2D.AddForce(force);

        if (_player.PlayerInput.HorizontalInput == 0)
        {
            StopPlayer();
        }
    }

    private void StopPlayer()
    {
        _rb2D.velocity = Vector2.zero;
    }

    // TODO: Check if the player is at the left edge of the screen and trying to move left
    // (Still Buggy)
    private bool IsAtLeftEdgeOfScreen()
    {  
        return Camera.main.WorldToViewportPoint(transform.position).x <= 0.1f;
    }
}
