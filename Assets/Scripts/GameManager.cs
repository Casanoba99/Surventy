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
        if (!transicion.gameObject.activeSelf) transicion.gameObject.SetActive(true);
        if (gm == null) gm = this;
    }
    #endregion

    Coroutine limpiarCoro, raton;
    Vector3 ratonPos;

    bool pOpen = false;
    [HideInInspector]
    public bool tD = false;
    [HideInInspector]
    public float tiempoDelta;

    [Header("Gameplay")]
    public Image barraTiempo;
    public bool start = false;
    public float tiempo;

    [Header("Rondas")]
    public int ronda;
    public int maxRondas = 10;

    [Header("Menus")]
    public GameObject seleccionarArma;
    public GameObject menuPausa;
    public GameObject botonReanudar;
    public VictoriaDerrota menuVD;
    [Space(5)]
    public Image transicion;

    private void Start()
    {
        if (!seleccionarArma.activeSelf)
        {
            seleccionarArma.SetActive(true);
            menuPausa.SetActive(false);
            menuVD.gameObject.SetActive(false);
        }

        transicion.CrossFadeAlpha(0, 1, true);
    }

    void Update()
    {
        // Capturas
        if (Input.GetKeyDown(KeyCode.F12))
        {
            string date = System.DateTime.Now.ToString("dd-MM-yy_HH-mm-ss");
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screenshot_" + date + ".png");
        }

        Start_RatonMovimiento();

        // Control del tiempo
        if (tD) tiempoDelta = Time.deltaTime;

        // Menu Pausa
        if (CanOpenPausa())
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {
                menuPausa.GetComponent<AudioSource>().Play();

                start = !start;
                tD = !tD;
                pOpen = !pOpen;
                menuPausa.SetActive(pOpen);

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(botonReanudar);
            }
        }

        // Cuenta atras
        if (start)
        {
            tiempo -= tiempoDelta;
            barraTiempo.fillAmount = tiempo / 20;
        }

        if (tiempo <= 0 && start == true && ronda < 10)
        {
            ronda++;
            start = false;
            tD = false;
            seleccionarArma.SetActive(true);
            seleccionarArma.GetComponent<SelectArma>().ImprimirCartas();
        }

        // TERMINAR RONDA 10
        //menuVD.gameObject.SetActive(true);
        //menuVD.Resolucion(true);
    }

    bool CanOpenPausa()
    {
        if (menuVD.gameObject.activeSelf || seleccionarArma.activeSelf) return false;
        else return true;
    }

    void Start_RatonMovimiento()
    {
        raton ??= StartCoroutine(RatonMovimiento());
    }

    IEnumerator RatonMovimiento()
    {
        ratonPos = Input.mousePosition;

        yield return new WaitForSeconds(.25f);

        if (Input.mousePosition != ratonPos) Cursor.visible = true;
        else Cursor.visible = false;

        raton = null;
    }
}
