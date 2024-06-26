using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central script for communication between the different player components
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerVisual _playerVisual;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerPhysics _playerPhysics;
    [SerializeField] private PlayerAnimation _playerAnimation;
    public PlayerVisual PlayerVisual => _playerVisual;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerInput PlayerInput => _playerInput;
    public PlayerPhysics PlayerPhysics => _playerPhysics;
    public PlayerAnimation PlayerAnimation => _playerAnimation;

    private void Awake()
    {
        Cursor.visible = false;
    }
}
