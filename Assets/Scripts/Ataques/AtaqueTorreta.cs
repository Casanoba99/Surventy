using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueTorreta : MonoBehaviour
{
    public enum Nivel { Uno, Dos, Tres, Cuatro }

    GameManager Manager => GameManager.gm;

    int proyectiles = 0;
    bool instanciados = false;

    [HideInInspector]
    public int nivelActual = 0;
    [HideInInspector]
    public int duracion = 0;

    public Nivel nivel;
    public GameObject prefb;
    public CartasSO carta;

    [Header("Stats")]
    public int da�o;
    public float cooldown;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidad;

    private void Start()
    {
        CambiarStats();
    }

    void Update()
    {
        if (Manager.start && !instanciados)
        {
            // Instanciar ataque
            InstanciarTorretas();
        }

        if (!Manager.start && !Manager.tD) instanciados = false;
    }

    //---------------------------------------------------------------------------------------------

    void InstanciarTorretas()
    {
        for (int i = 0; i < proyectiles; i++)
        {
            Vector3 pos;
            int x = Random.Range(-12, 13);
            int y = Random.Range(-6, 7);
            pos = new Vector3(x, y, 0);

            GameObject bot = Instantiate(prefb, pos, Quaternion.identity, transform);
            bot.name = carta.nombre;
        }

        instanciados = true;
    }

    public void CambiarStats()
    {
        nivelActual++;

        if (nivelActual == 1)
        {
            nivel = Nivel.Uno;
            proyectiles = 1;
        }
        else if (nivelActual == 2)
        {
            nivel = Nivel.Dos;
            proyectiles = 2;
        }
        else if (nivelActual == 3)
        {
            nivel = Nivel.Tres;
            proyectiles = 3;
        }
        else if (nivelActual == 4)
        {
            nivel = Nivel.Cuatro;
            proyectiles = 4;
        }

        switch (nivel)
        {
            case Nivel.Uno:
                da�o = carta.nivel[0].da�o;
                cooldown = carta.nivel[0].cooldown;
                areaDa�o = carta.nivel[0].areaDa�o;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidad = carta.nivel[0].velocidad;
                break;
            case Nivel.Dos:
                da�o = carta.nivel[1].da�o;
                cooldown = carta.nivel[1].cooldown;
                areaDa�o = carta.nivel[1].areaDa�o;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidad = carta.nivel[1].velocidad;
                break;
            case Nivel.Tres:
                da�o = carta.nivel[2].da�o;
                cooldown = carta.nivel[2].cooldown;
                areaDa�o = carta.nivel[2].areaDa�o;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidad = carta.nivel[2].velocidad;
                break;
            case Nivel.Cuatro:
                da�o = carta.nivel[3].da�o;
                cooldown = carta.nivel[3].cooldown;
                areaDa�o = carta.nivel[3].areaDa�o;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidad = carta.nivel[3].velocidad;
                break;
        }
    }
}
