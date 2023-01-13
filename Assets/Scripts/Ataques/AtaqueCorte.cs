using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueCorte : MonoBehaviour
{
    public enum Nivel { Uno, Dos, Tres, Cuatro }

    GameManager manager;
    AudioSource source;
    Coroutine slashCoro, targetCoro;

    [HideInInspector]
    public int nivelActual = 0;
    int proyectiles = 0;

    public Transform target;
    public GameObject prefb;
    public CartasSO carta;

    [Header("Stats")]
    public Nivel nivel;
    public int da�o;
    public float cooldown;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidad;

    private void Start()
    {
        manager = GameManager.gm;
        source = GetComponent<AudioSource>();

        CambiarStats();
    }

    void Update()
    {
        if (manager.start)
        {
            // Seleccionar objetivo
            Start_SelecTarget();
            // Instanciar ataque
            Start_Slash();
        }
    }

    //---------------------------------------------------------------------------------------------

    void Start_Slash()
    {
        slashCoro ??= StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {
        yield return new WaitForSeconds(cooldown);

        if (target && manager.start)
        {
            VariacionAtaque();
            source.Play();
        }

        slashCoro = null;
    }

    void VariacionAtaque()
    {
        Vector3 _target = target.position;
        Vector3 dir = _target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < proyectiles; i++)
        {
            GameObject clon = Instantiate(prefb, transform.position, Quaternion.AngleAxis(angle, Vector3.forward), transform);
            clon.name = "Slash";
            clon.transform.localScale = Vector3.one * areaDa�o;
            angle += 90f;
        }
    }

    void Start_SelecTarget()
    {
        targetCoro ??= StartCoroutine(SeleccionarTarget());
    }

    IEnumerator SeleccionarTarget()
    {
        Collider2D[] nearTargets = Physics2D.OverlapCircleAll(transform.position, radioAtaque, LayerMask.GetMask("Enemigo"));

        for (int i = 0; i < nearTargets.Length; i++)
        {
            if (Vector2.Distance(transform.position, nearTargets[i].transform.position) < (radioAtaque / 2))
            {
                target = nearTargets[i].transform;
            }
        }

        yield return new WaitForEndOfFrame();
        targetCoro = null;
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
            proyectiles = 4;
        }
        else if (nivelActual == 4)
        {
            nivel = Nivel.Cuatro;
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
