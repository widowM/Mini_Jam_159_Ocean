using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float _lastDirection = 1;
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private SpriteRenderer _characterSpriteRend;

    public SpriteRenderer CharacterSpriteRend => _characterSpriteRend;

    private void Update()
    {
        SetSpriteFacingDirection();
    }

    private void SetSpriteFacingDirection()
    {
        // Player sprite facing left or right keeping the last direction
        if (_player.PlayerInput.HorizontalInput != 0)
        {
            _lastDirection = _player.PlayerInput.HorizontalInput < 0 ? -1 : 1;
        }

        _spriteTransform.localScale = new Vector2(_lastDirection, 1);
    }
}