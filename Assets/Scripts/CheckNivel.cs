using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNivel : MonoBehaviour
{
    string nombre;

    public int nivel = 0;
    public CartasSO carta;

    [Header("Stats")]
    public int da�o;
    public float cooldown;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidad;

    public void SubirNivel()
    {
        nombre = transform.name;
        nivel++;

        switch (nivel)
        {
            case 1:
                da�o = carta.nivel[0].da�o;
                cooldown = carta.nivel[0].cooldown;
                areaDa�o = carta.nivel[0].areaDa�o;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidad = carta.nivel[0].velocidad;
                break;
            case 2:
                da�o = carta.nivel[1].da�o;
                cooldown = carta.nivel[1].cooldown;
                areaDa�o = carta.nivel[1].areaDa�o;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidad = carta.nivel[1].velocidad;
                break;
            case 3:
                da�o = carta.nivel[2].da�o;
                cooldown = carta.nivel[2].cooldown;
                areaDa�o = carta.nivel[2].areaDa�o;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidad = carta.nivel[2].velocidad;
                break;
            case 4:
                da�o = carta.nivel[3].da�o;
                cooldown = carta.nivel[3].cooldown;
                areaDa�o = carta.nivel[3].areaDa�o;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidad = carta.nivel[3].velocidad;
                break;
        }

        switch (nombre)
        {
            case "Bob the Bot":
                GetComponent<AtaqueOrbita>().CambiarStats();
                break;
            case "Gas Grenade":
                GetComponent<AtaqueGas>().CambiarStats();
                break;
            case "Laser Sword":
                GetComponent<AtaqueCorte>().CambiarStats();
                break;
            case "Shotgun":
                GetComponent<AtaqueBolas>().CambiarStats();
                break;
            case "Bot Turret":
                GetComponent<AtaqueTorreta>().CambiarStats();
                break;
        }
    }
}
