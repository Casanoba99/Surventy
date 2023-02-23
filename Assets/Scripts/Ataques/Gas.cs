using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    Coroutine targetCoro;
    GameManager Manager => GameManager.gm;
    ParticleSystem Ps => GetComponent<ParticleSystem>();
    CircleCollider2D Cc => GetComponent<CircleCollider2D>();
    AudioSource Source => GetComponent<AudioSource>();
    GameObject Luz => transform.GetChild(0).gameObject;

    bool explota = false;
    [HideInInspector]
    public Vector3 target;

    [Header("Stats")]
    public int da�o;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidad;

    void Start()
    {

        AtaqueGas aGas = GetComponentInParent<AtaqueGas>();

        da�o = aGas.da�o;
        areaDa�o = aGas.areaDa�o;
        radioAtaque = aGas.radioAtaque;
        velocidad = aGas.velocidad;

        StartCoroutine(SeleccionarTarget());

        Cc.radius *= areaDa�o;
        Luz.transform.localScale = Vector3.one * Cc.radius * 2;

        ParticleSystem.MainModule main = Ps.main;
        ParticleSystem.ShapeModule shape = Ps.shape;
        main.duration = aGas.duracion;
        shape.radius = Cc.radius;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, velocidad * Manager.tiempoDelta);

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
        Luz.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        Ps.Play();
        Source.Play();
        Cc.enabled = true;
        yield return new WaitForSeconds(Ps.main.duration + .25f);
        Destroy(gameObject);
    }
}
