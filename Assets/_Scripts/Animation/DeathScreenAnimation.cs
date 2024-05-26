using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathScreenAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _deathScreenRectTransform;
    [SerializeField] private Ease _ease;
    private void Start()
    {
        float delay = 1;
        StartCoroutine(DelayDeathScreenFalling(delay));
    }

    IEnumerator DelayDeathScreenFalling(float delay)
    {
        yield return new WaitForSeconds(delay);
        _deathScreenRectTransform.DOAnchorPosY(0, 2).SetEase(_ease);
    }
}
