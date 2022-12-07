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

    Coroutine limpiarCoro, raton;
    Vector3 ratonPos;

    bool pOpen = false;
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
        if (Input.GetKeyDown(KeyCode.F12))
        {
            string date = System.DateTime.Now.ToString("dd-MM-yy_HH-mm-ss");
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screenshot_" + date + ".png");
        }

        Start_RatonMovimiento();

        if (tD) tiempoDelta = Time.deltaTime;

        if (CanOpenPausa())
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {
                mPausa.GetComponent<AudioSource>().Play();

                start = !start;
                tD = !tD;
                pOpen = !pOpen;
                mPausa.SetActive(pOpen);

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(bReanudar);
            }
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

    bool CanOpenPausa()
    {
        if (mVD.gameObject.activeSelf || sArma.activeSelf) return false;
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
