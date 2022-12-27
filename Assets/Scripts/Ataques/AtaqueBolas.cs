using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBolas : MonoBehaviour
{
    public enum Nivel { Uno, Dos, Tres, Cuatro }

    GameManager manager => GameManager.gm;
    AudioSource source => GetComponent<AudioSource>();
    Coroutine targetCoro, bolasCoro;
    [HideInInspector]
    public int nivelActual = 0;
    int bolasDisparadas = 0;

    public Nivel nivel;
    public Transform target;
    public GameObject prefab;
    public CartasSO carta;

    [Header("Stats")]
    public int da�o;
    public float cooldown;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidad;

    [Header("Bolas")]
    public int vida;

    void Start()
    {
        CambiarStats();
    }

    void Update()
    {
        if (manager.start)
        {
            // Seleccionar objetivo
            Start_SelecTarget();
            // Instanciar ataque
            Start_Bolas();
        }

        if (transform.childCount != 0) Destroy(transform.GetChild(0).gameObject, vida);
    }

    void Start_Bolas()
    {
        bolasCoro ??= StartCoroutine(Bolas());
    }

    IEnumerator Bolas()
    {
        yield return new WaitForSeconds(cooldown);

        if (target && manager.start)
        {
            Rotacion();
            GameObject bolas = Instantiate(prefab, transform.position, transform.rotation, transform);

            for (int i = 0; i < bolasDisparadas; i++)
            {
                bolas.transform.GetChild(i).gameObject.SetActive(true);
            }

            source.Play();
        }

        bolasCoro = null;
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

    void Rotacion()
    {
        Vector3 t = target.position;
        Vector3 dir = t - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void CambiarStats()
    {
        nivelActual++;

        if (nivelActual == 1)
        {
            nivel = Nivel.Uno;
            bolasDisparadas = 3;
        }
        else if (nivelActual == 2)
        {
            nivel = Nivel.Dos;
            bolasDisparadas = 7;
        }
        else if (nivelActual == 3)
        {
            nivel = Nivel.Tres;
            bolasDisparadas = 12;
        }
        else if (nivelActual == 4)
        {
            nivel = Nivel.Cuatro;
            bolasDisparadas = 17;
        }

        switch (nivel)
        {
            case Nivel.Uno:
                da�o = carta.nivel[0].da�o;
                areaDa�o = carta.nivel[0].areaDa�o;
                radioAtaque = carta.nivel[0].radioAtaque;
                velocidad = carta.nivel[0].velocidad;
                break;
            case Nivel.Dos:
                da�o = carta.nivel[1].da�o;
                areaDa�o = carta.nivel[1].areaDa�o;
                radioAtaque = carta.nivel[1].radioAtaque;
                velocidad = carta.nivel[1].velocidad;
                break;
            case Nivel.Tres:
                da�o = carta.nivel[2].da�o;
                areaDa�o = carta.nivel[2].areaDa�o;
                radioAtaque = carta.nivel[2].radioAtaque;
                velocidad = carta.nivel[2].velocidad;
                break;
            case Nivel.Cuatro:
                da�o = carta.nivel[3].da�o;
                areaDa�o = carta.nivel[3].areaDa�o;
                radioAtaque = carta.nivel[3].radioAtaque;
                velocidad = carta.nivel[3].velocidad;
                break;
        }
    }
}
