using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _playerAnimator;

    private enum PlayerState
    {
        Idle = 0,
        Walking = 1,
        Pushing = 2
    }

    private PlayerState _currentState = PlayerState.Idle;


    private void Update()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                // Play idle animation
                _playerAnimator.SetBool("IsIdle", true);
                _playerAnimator.SetBool("IsWalking", false);
                _playerAnimator.SetBool("IsPushing", false);
                break;

            case PlayerState.Walking:
                // Play walking animation
                _playerAnimator.SetBool("IsIdle", false);
                _playerAnimator.SetBool("IsWalking", true);
                _playerAnimator.SetBool("IsPushing", false);
                break;

            case PlayerState.Pushing:
                // Play pushing animation
                _playerAnimator.SetBool("IsIdle", false);
                _playerAnimator.SetBool("IsWalking", false);
                _playerAnimator.SetBool("IsPushing", true);
                break;

            default:
                Debug.LogWarning("Invalid player state!");
                break;
        }
    }

    public void ChangeState(int newState)
    {
        _currentState = (PlayerState)newState;
    }
}