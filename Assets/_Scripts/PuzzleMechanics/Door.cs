using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _levelCompletedSO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            _levelCompletedSO.RaiseEvent();
        }
    }
}