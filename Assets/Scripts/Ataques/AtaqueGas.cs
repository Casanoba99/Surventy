using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueGas : MonoBehaviour
{
    public enum Nivel { Uno, Dos, Tres, Cuatro }

    GameManager manager => GameManager.gm;
    AudioSource source => GetComponent<AudioSource>();
    Coroutine gasCoro;

    int proyectiles = 0;

    [HideInInspector]
    public int nivelActual = 0;
    [HideInInspector]
    public int duracion = 0;

    public Nivel nivel;
    public GameObject prefb;
    public CartasSO carta;

    [Header("Stats")]
    public int daño;
    public float cooldown;
    public float areaDaño;
    public float radioAtaque;
    public float velocidad;

    private void Start()
    {
        CambiarStats();
    }

    void Update()
    {
        if (manager.start)
        {
            // Instanciar ataque
            Start_Gas();
        }
    }

    //---------------------------------------------------------------------------------------------

    void Start_Gas()
    {
        gasCoro ??= StartCoroutine(Gas());
    }

    IEnumerator Gas()
    {
        yield return new WaitForSeconds(cooldown);

        if (EnemigosCerca() && manager.start)
        {
            for (int i = 0; i < proyectiles; i++)
            {
                GameObject gas = Instantiate(prefb, transform.position, Quaternion.identity, transform);
                gas.name = carta.nombre;
            }
            source.Play();
        }

        gasCoro = null;
    }


    bool EnemigosCerca()
    {
        Collider2D[] nearTargets = Physics2D.OverlapCircleAll(transform.position, radioAtaque, LayerMask.GetMask("Enemigo"));
        
        for (int i = 0; i < nearTargets.Length; i++)
        {
            if (Vector3.Distance(transform.position, nearTargets[i].transform.position) < (radioAtaque / 2))
                return true;
        }

        return false;
    }

    public void CambiarStats()
    {
        nivelActual = GetComponent<CheckNivel>().nivel;

        if (nivelActual == 1)
        {
            nivel = Nivel.Uno;
            duracion = proyectiles = 1;
        }
        else if (nivelActual == 2)
        {
            nivel = Nivel.Dos;
            duracion = proyectiles = 2;
        }
        else if (nivelActual == 3)
        {
            nivel = Nivel.Tres;
            duracion = 3;
        }
        else if (nivelActual == 4)
        {
            nivel = Nivel.Cuatro;
            duracion = 4;
            proyectiles = 3;
        }

        switch (nivel)
        {
            case Nivel.Uno:
                daño = carta.nivel[0].daño;
                cooldown = carta.nivel[0].cooldown;
                areaDaño = carta.nivel[0].areaDaño;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidad = carta.nivel[0].velocidad;
                break;
            case Nivel.Dos:
                daño = carta.nivel[1].daño;
                cooldown = carta.nivel[1].cooldown;
                areaDaño = carta.nivel[1].areaDaño;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidad = carta.nivel[1].velocidad;
                break;
            case Nivel.Tres:
                daño = carta.nivel[2].daño;
                cooldown = carta.nivel[2].cooldown;
                areaDaño = carta.nivel[2].areaDaño;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidad = carta.nivel[2].velocidad;
                break;
            case Nivel.Cuatro:
                daño = carta.nivel[3].daño;
                cooldown = carta.nivel[3].cooldown;
                areaDaño = carta.nivel[3].areaDaño;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidad = carta.nivel[3].velocidad;
                break;
        }
    }
}
