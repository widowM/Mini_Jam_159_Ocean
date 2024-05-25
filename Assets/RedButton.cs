using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    [SerializeField] private GameObject _buttonDown;
    [SerializeField] private GameObject _doorClosed;
    [SerializeField] private GameObject _doorOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _buttonDown.SetActive(true);
            _doorClosed.SetActive(false);
            _doorOpen.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}
