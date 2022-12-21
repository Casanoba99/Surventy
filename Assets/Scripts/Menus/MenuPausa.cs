using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    GameManager manager;

    public GameObject reanudar;
    [Space(5)]
    public GameObject opciones;
    public GameObject handMusica;

    [Header("Audio")]
    public AudioSource source;
    public AudioMixer aMixer;
    [Space(5)]
    public Slider sMusica;
    public Slider sSonido;

    private void Start()
    {
        manager = GameManager.gm;

        // Audio
        sMusica.value = PlayerPrefs.GetFloat("Musica");
        sSonido.value = PlayerPrefs.GetFloat("Sonido");
    }

    public void Reanudar()
    {
        source.Play();
        manager.start = !manager.start;
        manager.tD = !manager.tD;
        manager.pOpen = !manager.pOpen;
        gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(reanudar);
    }

    public void Opciones()
    {
        source.Play();
        opciones.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(handMusica);
    }

    public void CerrarOpciones()
    {
        source.Play();
        opciones.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(reanudar);
    }

    public void Musica(float valor)
    {
        aMixer.SetFloat("Musica", valor);
        PlayerPrefs.SetFloat("Musica", valor);
    }

    public void Sonido(float valor)
    {
        source.Play();
        aMixer.SetFloat("Sonido", valor);
        PlayerPrefs.SetFloat("Sonido", valor);
    }
}
