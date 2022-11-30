using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager gm;
    private void Awake()
    {
        if (gm == null) gm = this;
    }
    #endregion

    Coroutine limpiarCoro;

    [HideInInspector]
    public bool tD = false;
    [HideInInspector]
    public float tiempoDelta;

    [Header("Gameplay")]
    public Image bTiempo;
    public bool start = false;
    public float tiempo;
    [Space(5)]
    public GameObject sArma;
    public GameObject mPausa;
    public GameObject bReanudar;
    public VictoriaDerrota mVD;
    [Space(5)]
    public Image tran;

    private void Start()
    {
        if (!sArma.activeSelf)
        {
            sArma.SetActive(true);
            mPausa.SetActive(false);
            mVD.gameObject.SetActive(false);
        }

        tran.CrossFadeAlpha(0, 1, true);
    }

    void Update()
    {
        if (tD) tiempoDelta = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            mPausa.GetComponent<AudioSource>().Play();

            start = !start;
            tD = !tD;
            mPausa.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(bReanudar);
        }

        // Cuenta atras
        if (start)
        {
            tiempo -= tiempoDelta;
            bTiempo.fillAmount = tiempo / 20;
        }

        if (tiempo <= 0 && start == true)
        {
            start = false;
            tD = false;
            mVD.gameObject.SetActive(true);
            mVD.Resolucion(true);
        }
    }
}
