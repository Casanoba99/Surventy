using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueGas : MonoBehaviour
{

    GameManager manager => GameManager.gm;
    AudioSource source => GetComponent<AudioSource>();
    Coroutine gasCoro;

    int proyectiles = 0;

    [HideInInspector]
    public int duracion = 0;

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
        CheckNivel check = GetComponent<CheckNivel>();

        daño = check.daño;
        cooldown = check.cooldown;
        areaDaño = check.areaDaño;
        radioAtaque = check.radioAtaque;
        velocidad = check.velocidad;

        if (check.nivel == 1)
        {
            duracion = proyectiles = 1;
        }
        else if (check.nivel == 2)
        {
            duracion = proyectiles = 2;
        }
        else if (check.nivel == 3)
        {
            duracion = 3;
        }
        else if (check.nivel == 4)
        {
            duracion = 4;
            proyectiles = 3;
        }
    }
}
