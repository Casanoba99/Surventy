using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [HideInInspector]
    public bool pOpen = false;
    [HideInInspector]
    public bool tD = false;
    [HideInInspector]
    public float tiempoDelta;

    public Transform player;

    [Header("Gameplay")]
    public Image barraTiempo;
    public bool start = false;
    public float tiempo;

    [Header("Rondas")]
    public TextMeshProUGUI rondasTexto;
    public int ronda;
    public int maxRondas = 10;
    [Space(10)]
    public SpawnEnemigos spawn;
    public int incrementoEnemigos;
    public int incrementoEnemigosMin;

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

        rondasTexto.text = "" + ronda;
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

        // Raton
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

        // Termina la Ronda
        if (tiempo <= 0 && start == true && ronda < 10)
        {
            TerminaRonda();
        }

        // TERMINAR RONDA 10
        //menuVD.gameObject.SetActive(true);
        //menuVD.Resolucion(true);
    }

    public void EmpezarRonda()
    {
        tiempo = 20;
        barraTiempo.fillAmount = 1;

        ronda++;
        rondasTexto.text = "" + ronda;
        start = true;
        tD = true;
    }
    
    void TerminaRonda()
    {
        start = false;
        tD = false;

        spawn.cantidad += incrementoEnemigos;
        spawn.min += incrementoEnemigosMin;
        spawn.EliminarEnemigos();

        player.position = Vector2.zero;

        seleccionarArma.SetActive(true);
        seleccionarArma.GetComponent<SelectArma>().ImprimirCartas();
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
