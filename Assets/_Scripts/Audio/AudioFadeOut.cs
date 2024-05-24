using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeDuration = 2f;

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutAudio());
    }

    private IEnumerator FadeOutAudio()
    {
        float startVolume = _audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            float t = elapsedTime / _fadeDuration;
            _audioSource.volume = Mathf.Lerp(startVolume, 0f, t);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
    }
}