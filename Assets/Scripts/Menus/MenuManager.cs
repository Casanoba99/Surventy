using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class MenuManager : MonoBehaviour
{
    public GameObject mOpciones;
    public GameObject creditos;
    public Image transicion;

    [Header("Audio")]
    public AudioSource source;
    public AudioMixer mixer;
    [Space(5)]
    public Slider sMusica;
    public Slider sSonido;

    [Header("Event System")]
    public GameObject bEmpezar;
    public GameObject bCreditos;

    void Start()
    {
        transicion.CrossFadeAlpha(0, 1, false);

        if (mOpciones.activeSelf) mOpciones.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bEmpezar);

        sMusica.value = PlayerPrefs.GetFloat("Musica");
        sSonido.value = PlayerPrefs.GetFloat("Sonido");
        mixer.SetFloat("Musica", sMusica.value);
        mixer.SetFloat("Sonido", sSonido.value);
    }

    public void Empezar()
    {
        MusicaManager.mManager.CambiarMusicaGP();
        transicion.CrossFadeAlpha(255, 1, false);

        source.Play();
        SceneManager.LoadScene(1);
    }

    public void AbrirOpciones()
    {
        source.Play();

        mOpciones.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(sMusica.gameObject);
    }

    public void CerrarOpciones()
    {
        source.Play();

        mOpciones.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bEmpezar);
    }

    public void Musica(float valor)
    {
        mixer.SetFloat("Musica", valor);
        PlayerPrefs.SetFloat("Musica", valor);
    }

    public void Sonido(float valor)
    {
        if (mOpciones.activeSelf) source.Play();
        mixer.SetFloat("Sonido", valor);
        PlayerPrefs.SetFloat("Sonido", valor);
    }

    public void CreditosAbrir()
    {
        source.Play();
        
        creditos.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bCreditos);
    }

    public void CreditosCerrar()
    {
        source.Play();

        creditos.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bEmpezar);
    }

    public void Quitar()
    {
        source.Play();
        Application.Quit();
    }
}
