using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueGas : MonoBehaviour
{
    GameManager manager;
    AudioSource source;
    Coroutine gasCoro, targetCoro;

    public Transform target;
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
        manager = GameManager.gm;
        source = GetComponent<AudioSource>();

        daño = carta.nivel[0].daño;
        cooldown = carta.nivel[0].cooldown;
        areaDaño = carta.nivel[0].areaDaño;
        radioAtaque = carta.nivel[0].radioAtaque;
        velocidad = carta.nivel[0].velocidad;
    }

    void Update()
    {
        if (manager.start)
        {
            // Seleccionar objetivo
            Start_SelecTarget();
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

        if (target && manager.start)
        {
            _ = Instantiate(prefb, transform.position, Quaternion.identity, transform);
            source.Play();
        }

        gasCoro = null;
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
}
