using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private bool _isTouchingRock = false;

    public bool IsTouchingRock => _isTouchingRock;

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
