using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueOrbita : MonoBehaviour
{
    public enum Nivel { Uno, Dos, Tres, Cuatro }

    GameManager Manager => GameManager.gm;
    int nivelActual = 0;

    public Nivel nivel;
    public CartasSO carta;

    [Header("Stats")]
    public int da�o;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidadRot;

    private void Start()
    {
        CambiarStats();
    }

    void Update()
    {
        if (Manager.start)
        {
            transform.Rotate(Vector3.forward, velocidadRot * Manager.tiempoDelta);
        }
    }

    public void CambiarStats()
    {
        nivelActual++;

        if (nivelActual == 1)
        {
            nivel = Nivel.Uno;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (nivelActual == 2)
        {
            nivel = Nivel.Dos;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (nivelActual == 3)
        {
            nivel = Nivel.Tres;
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (nivelActual == 4)
        {
            nivel = Nivel.Cuatro;
            transform.GetChild(3).gameObject.SetActive(true);
        }

        switch (nivel)
        {
            case Nivel.Uno:
                da�o = carta.nivel[0].da�o;
                areaDa�o = carta.nivel[0].areaDa�o;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidadRot = carta.nivel[0].velocidad;
                break;
            case Nivel.Dos:
                da�o = carta.nivel[1].da�o;
                areaDa�o = carta.nivel[1].areaDa�o;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidadRot = carta.nivel[1].velocidad;
                break;
            case Nivel.Tres:
                da�o = carta.nivel[2].da�o;
                areaDa�o = carta.nivel[2].areaDa�o;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidadRot = carta.nivel[2].velocidad;
                break;
            case Nivel.Cuatro:
                da�o = carta.nivel[3].da�o;
                areaDa�o = carta.nivel[3].areaDa�o;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidadRot = carta.nivel[3].velocidad;
                break;
        }

        for (int i = 0; i < nivelActual; i++)
        {
            Debug.Log("For");
            Transform child = transform.GetChild(i).transform;
            if (child.GetSiblingIndex() <= 1)
            {
                Debug.Log("Radio");
                child.GetComponent<Bob>().CambiarRadio(new Vector3(radioAtaque, 0, 0));
            }
            else if (child.GetSiblingIndex() >= 2)
            {
                child.GetComponent<Bob>().CambiarRadio(new Vector3(0, radioAtaque, 0));

            }

            child.localScale = Vector3.one * areaDa�o;
        }
    }
}

