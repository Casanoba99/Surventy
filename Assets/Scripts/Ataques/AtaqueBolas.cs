using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBolas : MonoBehaviour
{
    GameManager manager;
    Coroutine targetCoro, bolasCoro;

    public Transform target;
    public GameObject prefab;
    public int cooldown;
    public int radio;

    [Header("Bolas")]
    public int velocidad;
    public int vida;

    void Start()
    {
        manager = GameManager.gm;
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
            _ = Instantiate(prefab, transform.position, transform.rotation, transform);
        }

        bolasCoro = null;
    }

    void Start_SelecTarget()
    {
        targetCoro ??= StartCoroutine(SeleccionarTarget());
    }

    IEnumerator SeleccionarTarget()
    {
        Collider2D[] nearTargets = Physics2D.OverlapCircleAll(transform.position, radio, LayerMask.GetMask("Enemigo"));

        for (int i = 0; i < nearTargets.Length; i++)
        {
            if (Vector2.Distance(transform.position, nearTargets[i].transform.position) < (radio / 2))
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
}
