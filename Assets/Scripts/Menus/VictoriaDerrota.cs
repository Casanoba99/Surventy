using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoriaDerrota : MonoBehaviour
{
    public GameObject[] vDText;
    public GameObject botones;
    public Image tran;

    public void Resolucion(bool v)
    {
        if (v)
        {
            vDText[0].SetActive(true);
            botones.SetActive(true);
        }
        else
        {
            vDText[1].SetActive(true);
            botones.SetActive(true);
        }
            
    }

    public void Reintentar()
    {
        tran.CrossFadeAlpha(255, 1, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        tran.CrossFadeAlpha(255, 1, true);
        SceneManager.LoadScene(0);
    }
}
