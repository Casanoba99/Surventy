using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public CartaPrfb[] cartaPrfb;
    public Cartas[] cartas;

    void Start()
    {
        ints = new List<int>(new int[cartaPrfb.Length]);
        for (int i = 0; i < ints.Count; i++)
        {
            ints[i] = -1;
        }

        ImprimirCartas();
    }

    void ImprimirCartas()
    {
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
    }
}
