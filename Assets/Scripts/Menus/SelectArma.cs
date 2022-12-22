using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public struct CartaPrfb
{
    public CartasSO carta;
    public GameObject prfb;
}

public class SelectArma : MonoBehaviour
{
    List<int> ints= new List<int>();
    int nCarta;
    Transform Player => GameObject.Find("Player").transform;

    public CartaPrfb[] cartaPrfb;
    public Cartas[] cartas;

    void Start()
    {
        ints = new List<int>(new int[cartaPrfb.Length]);
        ImprimirCartas();
    }

    public void ImprimirCartas()
    {
        LimpiarCarta();

        for (int i = 0; i < cartas.Length; i++)
        {
            nCarta = UnityEngine.Random.Range(0, cartaPrfb.Length);
            while (ints.Contains(nCarta))
            {
                nCarta = UnityEngine.Random.Range(0, cartaPrfb.Length);
            }
            ints[i] = nCarta;

            cartas[i].carta = cartaPrfb[nCarta].carta;
            cartas[i].prfb = cartaPrfb[nCarta].prfb;

            cartas[i].AsignarValores();
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(cartas[0].gameObject);
    }

    void LimpiarCarta()
    {
        for (int i = 0; i < ints.Count; i++)
        {
            ints[i] = -1;
        }

        for (int i = 0; i < cartas.Length; i++)
        {
            cartas[i].carta = null;
            cartas[i].prfb = null;
            cartas[i].LimpiarValores();
        }
    }

    bool ComprobarObjetos(GameObject carta)
    {
        // Aaaaaaaaaaahhhhhhhh
        for (int i = 0; i < Player.childCount; i++)
        {
            if (Player.GetChild(i).gameObject == carta)
            {
                return false;
            }
        }

        return true;
    }
}
