using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Gas : MonoBehaviour
{
    Coroutine targetCoro;
    GameManager manager => GameManager.gm;
    ParticleSystem ps => GetComponent<ParticleSystem>();
    CircleCollider2D cc => GetComponent<CircleCollider2D>();
    GameObject luz => transform.GetChild(0).gameObject;

    bool explota = false;
    [HideInInspector]
    public Vector3 target;

    [Header("Stats")]
    public int daño;
    public float areaDaño;
    public float radioAtaque;
    public float velocidad;

    void Start()
    {

        AtaqueGas aGas = GetComponentInParent<AtaqueGas>();

        daño = aGas.daño;
        areaDaño = aGas.areaDaño;
        radioAtaque = aGas.radioAtaque;
        velocidad = aGas.velocidad;

        StartCoroutine(SeleccionarTarget());

        cc.radius *= areaDaño;
        luz.transform.localScale = Vector3.one * cc.radius * 2;

        ParticleSystem.MainModule main = ps.main;
        ParticleSystem.ShapeModule shape = ps.shape;
        main.duration = aGas.duracion;
        shape.radius = cc.radius;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, velocidad * manager.tiempoDelta);

        if (transform.position == target && !explota)
        {
            explota = true;
            StartCoroutine(Explosion());
        }
    }

    IEnumerator SeleccionarTarget()
    {
        Collider2D[] nearTargets = Physics2D.OverlapCircleAll(transform.position, radioAtaque, LayerMask.GetMask("Enemigo"));

        Transform rTarget;
        do
        {
            rTarget = nearTargets[Random.Range(0, nearTargets.Length)].transform;
        } 
        while (Vector3.Distance(transform.position, rTarget.position) > (radioAtaque / 2));

        target = rTarget.position;

        yield return new WaitForEndOfFrame();
    }

    IEnumerator Explosion()
    {
        luz.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        ps.Play();

        cc.enabled = true;
        yield return new WaitForSeconds(ps.main.duration + .25f);
        Destroy(gameObject);
    }
}
