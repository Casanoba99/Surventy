using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    public static MusicaManager mManager;
    private void Awake()
    {
        if (mManager == null)
        {
            mManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public AudioSource menuSource;
    public AudioSource gameSource;

    void Start()
    {
        StartCoroutine(FadeInMusicaUnaPista(menuSource, 1f));
    }

    public void CambiarMusicaGP()
    {
        StartCoroutine(FadeMusica(menuSource, gameSource, 0, 1));
    }

    public void CambiarMusicaM()
    {
        StartCoroutine(FadeMusica(gameSource, menuSource, 0, 1));
    }

    public IEnumerator FadeMusica(AudioSource source1, AudioSource source2, float min2, float max2)
    {
        float timeToFade = 0.25f;
        float timeElapsed = 0;

        source2.Play();

        while (timeElapsed < timeToFade)
        {
            source2.volume = Mathf.Lerp(min2, max2, timeElapsed / timeToFade);
            source1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        source1.Stop();
    }

    public IEnumerator FadeInMusicaUnaPista(AudioSource source, float tiempoFade)
    {

        float timeToFade = tiempoFade;
        float timeElapsed = 0;

        source.Play();

        while (timeElapsed < timeToFade)
        {
            source.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
