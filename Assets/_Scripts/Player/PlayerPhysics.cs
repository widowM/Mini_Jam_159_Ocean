using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] Transform _pushCheckTransform;
    [SerializeField] private LayerMask _pushCheckLayerMask;
    private bool _isTouchingRock = false;
    private bool _isPushingRock = false;
    private bool _isPushingBounds = false;
    [SerializeField] private AudioSource _rockPushingAudioSource;
    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _gameOverSO;
    public bool IsTouchingRock => _isTouchingRock;
    public bool IsPushingRock => _isPushingRock;
    public AudioSource RockPushingAudioSource => _rockPushingAudioSource;

    private void Update()
    {
        if (RockPushingCheck())
        {
            if (!_isPushingRock)
            {
                _rockPushingAudioSource.Play();
            }
            _isPushingRock = true;
        }
        else
        {
            if (_isPushingRock)
            {
                _rockPushingAudioSource.Stop();
            }
            _isPushingRock = false;
        }

        if (_player.PlayerMovement.IsPushingScreenBounds)
        {
            if (!_isPushingBounds)
            {
                _rockPushingAudioSource.Play();
            }
            _isPushingBounds = true;
        }
        else
        {
            if (!_isPushingBounds && !IsPushingRock)
            {
                _rockPushingAudioSource.Stop();
            }
            _isPushingBounds = false;
        }
    }

    private bool RockPushingCheck()
    {
        Vector3 localDirection = Vector3.right;

        Vector3 worldDirection = _pushCheckTransform.TransformDirection(localDirection);

        Ray ray = new Ray(_pushCheckTransform.position, worldDirection);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.3f, _pushCheckLayerMask);


        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _isTouchingRock = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _isTouchingRock = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            _gameOverSO.RaiseEvent();
        }
    }
}
