using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueCorte : MonoBehaviour
{

    GameManager manager;
    AudioSource source;
    Coroutine slashCoro, targetCoro;

    int proyectiles = 0;

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
        // Rotacion
        Vector3 _target = target.position;
        Vector3 dir = _target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Instanciacion
        for (int i = 0; i < proyectiles; i++)
        {
            GameObject clon = Instantiate(prefb, transform.position, Quaternion.AngleAxis(angle, Vector3.forward), transform);
            clon.name = "Slash";
            clon.transform.localScale = Vector3.one * areaDaño;
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
        CheckNivel check = GetComponent<CheckNivel>();

        daño = check.daño;
        cooldown = check.cooldown;
        areaDaño = check.areaDaño;
        radioAtaque = check.radioAtaque;
        velocidad = check.velocidad;

        if (check.nivel == 1)
        {
            proyectiles = 1;
        }
        else if (check.nivel == 2)
        {
            proyectiles = 2;
        }
        else if (check.nivel == 3)
        {
            proyectiles = 4;
        }

    }
}
