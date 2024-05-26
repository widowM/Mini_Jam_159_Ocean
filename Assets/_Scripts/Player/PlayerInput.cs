using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float _horizontalInput;
    private bool _pressedJumpButton;
    private bool _keepPressingJumpButton;

    // Share horizontal input with the rest of the components
    public float HorizontalInput => _horizontalInput;
    public bool PressedJumpButton => _pressedJumpButton;
    public bool KeepPressingJumpButton => _keepPressingJumpButton;

    private void Update()
    {
        _horizontalInput = GetHorizontalInput();
        _pressedJumpButton = GetJumpButtonInput();
        _keepPressingJumpButton = GetKeepPressingJumpButtonInput();
    }

    private float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    private bool GetJumpButtonInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private bool GetKeepPressingJumpButtonInput()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
