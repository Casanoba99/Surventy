using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VictoriaDerrota : MonoBehaviour
{
    AudioSource source;

    public GameObject[] vDText;
    public GameObject botones;
    public Image tran;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Resolucion(bool v)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botones.transform.GetChild(0).gameObject);

        if (v && !vDText[1].activeSelf)
        {
            vDText[0].SetActive(true);
        }
        else if (!v && !vDText[0].activeSelf)
        {
            vDText[1].SetActive(true);
        }
        botones.SetActive(true);
    }

    public void Reintentar()
    {
        source.Play();
        tran.CrossFadeAlpha(255, 1, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        MusicaManager.mManager.CambiarMusicaM();
        source.Play();
        tran.CrossFadeAlpha(255, 1, true);
        SceneManager.LoadScene(0);
    }
}
