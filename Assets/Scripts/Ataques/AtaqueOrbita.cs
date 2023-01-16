using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueOrbita : MonoBehaviour
{
    public enum Nivel { Uno, Dos, Tres, Cuatro }

    GameManager Manager => GameManager.gm;
    [HideInInspector]
    public int nivelActual = 0;

    public Nivel nivel;
    public CartasSO carta;

    [Header("Stats")]
    public int daño;
    public float areaDaño;
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
        else
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void CambiarStats()
    {
        // Aaumentar Nivel
        nivelActual = GetComponent<CheckNivel>().nivel;

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

        // Cambiar los stats
        switch (nivel)
        {
            case Nivel.Uno:
                daño = carta.nivel[0].daño;
                areaDaño = carta.nivel[0].areaDaño;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidadRot = carta.nivel[0].velocidad;
                break;
            case Nivel.Dos:
                daño = carta.nivel[1].daño;
                areaDaño = carta.nivel[1].areaDaño;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidadRot = carta.nivel[1].velocidad;
                break;
            case Nivel.Tres:
                daño = carta.nivel[2].daño;
                areaDaño = carta.nivel[2].areaDaño;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidadRot = carta.nivel[2].velocidad;
                break;
            case Nivel.Cuatro:
                daño = carta.nivel[3].daño;
                areaDaño = carta.nivel[3].areaDaño;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidadRot = carta.nivel[3].velocidad;
                break;
        }

        // Aumentar ladistancia desde el centro
        for (int i = 0; i < nivelActual; i++)
        {
            Transform child = transform.GetChild(i).transform;
            // Eje X
            if (child.GetSiblingIndex() <= 1)
            {
                child.GetComponent<Bob>().CambiarRadio(new Vector3(radioAtaque, 0, 0));
            }
            // Eje Y
            else if (child.GetSiblingIndex() >= 2)
            {
                child.GetComponent<Bob>().CambiarRadio(new Vector3(0, radioAtaque, 0));

            }

            // Aumentra tamaño
            child.localScale = Vector3.one * areaDaño;
        }
    }
}

