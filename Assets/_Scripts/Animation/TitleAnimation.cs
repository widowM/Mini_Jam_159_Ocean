using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private Transform _title;

    private void Start()
    {
        StartCoroutine(StartTitleAnimation()); 
    }

    IEnumerator StartTitleAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        _title.DOScale(new Vector2(1.05f, 1.05f), 15).SetLoops(-1, LoopType.Yoyo);
    }
}
