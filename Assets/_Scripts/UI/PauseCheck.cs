using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCheck : MonoBehaviour
{
    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _gamePausedSO;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.IsPaused)
        {
            OnGamePaused();
        }
    }

    private void OnGamePaused()
    {
        _gamePausedSO.RaiseEvent();
    }
}