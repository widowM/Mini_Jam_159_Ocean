using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _loadNextSceneSO;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        _fadeImage.DOFade(0, 3);
    }

    public void FadeOut()
    {
        Time.timeScale = 0;
        _fadeImage.DOFade(1, 3).SetUpdate(true).OnComplete(_loadNextSceneSO.RaiseEvent);
    }
}
