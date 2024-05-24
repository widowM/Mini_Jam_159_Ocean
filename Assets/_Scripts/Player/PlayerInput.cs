using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float _horizontalInput;

    // Share horizontal input with the rest of the components
    public float HorizontalInput => _horizontalInput;

    private void Update()
    {
        _horizontalInput = GetInput();
    }

    private float GetInput()
    {
        return Input.GetAxis("Horizontal");
    }
}
