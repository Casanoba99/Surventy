using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager gm;
    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gm);
        }
        else Destroy(gameObject);
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

    void Update()
    {
        if (tD) tiempoDelta = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            start = !start;
            tD = !tD;
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
        }
    }
}
