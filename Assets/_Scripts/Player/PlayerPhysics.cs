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

    public bool IsTouchingRock => _isTouchingRock;
    public bool IsPushingRock => _isPushingRock;

    private void Update()
    {
        if (RockPushingCheck())
        {
            _isPushingRock = true;
        }
        else
        {
            _isPushingRock = false;
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
}
