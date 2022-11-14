using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool tD = false;
    [HideInInspector]
    public float tiempoDelta;

    [Header("Gameplay")]
    public SpawnEnemigos spawn;
    public bool start = false;
    public float tiempoTotal;
    public float tiempo;

    void Start()
    {

    }

    void Update()
    {
        if (tD) tiempoDelta = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !start)
        {
            start = true;
            tD = true;
        }

        if (start)
        {
            tiempo += tiempoDelta;
        }

        if (tiempo >= tiempoTotal)
        {
            start = false;
            tD = false;
            Limpiar();
        }
    }

    void Limpiar()
    {
        limpiarCoro ??= StartCoroutine(LimpiarEscena());
    }

    IEnumerator LimpiarEscena()
    {
        int n = spawn.hijos;

        for (int i = 0; i < n; i++)
        {
            Destroy(spawn.transform.GetChild(0).gameObject);
            yield return new WaitForEndOfFrame();
        }

        limpiarCoro = null;
    }
}
