using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNivel : MonoBehaviour
{
    public int nivel = 0;
    public CartasSO carta;

    [Header("Stats")]
    public int daño;
    public float cooldown;
    public float areaDaño;
    public float radioAtaque;
    public float velocidad;

    public void SubirNivel()
    {
        nivel++;

        switch (nivel)
        {
            case 1:
                daño = carta.nivel[0].daño;
                cooldown = carta.nivel[0].cooldown;
                areaDaño = carta.nivel[0].areaDaño;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidad = carta.nivel[0].velocidad;
                break;
            case 2:
                daño = carta.nivel[1].daño;
                cooldown = carta.nivel[1].cooldown;
                areaDaño = carta.nivel[1].areaDaño;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidad = carta.nivel[1].velocidad;
                break;
            case 3:
                daño = carta.nivel[2].daño;
                cooldown = carta.nivel[2].cooldown;
                areaDaño = carta.nivel[2].areaDaño;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidad = carta.nivel[2].velocidad;
                break;
            case 4:
                daño = carta.nivel[3].daño;
                cooldown = carta.nivel[3].cooldown;
                areaDaño = carta.nivel[3].areaDaño;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidad = carta.nivel[3].velocidad;
                break;
        }
    }
}
