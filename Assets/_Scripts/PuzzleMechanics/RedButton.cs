using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    [SerializeField] private GameObject _buttonDown;
    [SerializeField] private GameObject _doorClosed;
    [SerializeField] private GameObject _doorOpen;
    [SerializeField] private AudioClip _buttonPressedSFX;
    [SerializeField] private AudioSource _buttonAudioSource;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _doorOpeningSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _buttonAudioSource.PlayOneShot(_buttonPressedSFX);
            _buttonDown.SetActive(true);
            //_doorClosed.SetActive(false);
            //_doorOpen.SetActive(true);
            OnDoorOpening();
            gameObject.SetActive(false);
        }
    }

    private void OnDoorOpening()
    {
        _doorOpeningSO.RaiseEvent();
    }
}
