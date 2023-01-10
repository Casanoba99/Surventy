using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTorreta : MonoBehaviour
{
    Coroutine disparoCoro, targetCoro;

    GameManager Manager => GameManager.gm;

    [HideInInspector]
    public Transform target;

    public Transform shootPos;
    public GameObject shoot;

    [Header("Stats")]
    public int daño;
    public float cooldown;
    public float areaDaño;
    public float radioAtaque;
    public float velocidad;

    void Start()
    {
        AtaqueTorreta torreta = transform.GetComponentInParent<AtaqueTorreta>();

        daño = torreta.daño;
        cooldown = torreta.cooldown;
        areaDaño = torreta.areaDaño;
        radioAtaque = torreta.radioAtaque;
        velocidad = torreta.velocidad;

        transform.parent = null;
    }

    void Update()
    {
        if (Manager.start)
        {
            Start_SelecTarget();
            Start_Disparo();
        }

        if (!Manager.start)
        {
            Destroy(gameObject);
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
            if (Vector2.Distance(transform.position, nearTargets[i].transform.position) < radioAtaque)
            {
                target = nearTargets[i].transform;
            }
        }

        yield return new WaitForEndOfFrame();
        targetCoro = null;
    }

    void Start_Disparo()
    {
        disparoCoro ??= StartCoroutine(Disparo());
    }

    IEnumerator Disparo()
    {
        yield return new WaitForSeconds(cooldown);

        if (target)
        {
            GameObject _shoot = Instantiate(shoot, shootPos.position, Quaternion.identity, transform);
            _shoot.name = "Shoot";
        }

        disparoCoro = null;
    }
}
