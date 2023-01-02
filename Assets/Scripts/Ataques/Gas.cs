using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Gas : MonoBehaviour
{
    GameManager manager => GameManager.gm;
    ParticleSystem ps => GetComponent<ParticleSystem>();
    CircleCollider2D cc => GetComponent<CircleCollider2D>();
    GameObject luz => transform.GetChild(0).gameObject;

    bool explota = false;
    [HideInInspector]
    public Vector3 target;

    [Header("Stats")]
    public int da�o;
    public float areaDa�o;
    public float velocidad;

    void Start()
    {
        AtaqueGas aGas = GetComponentInParent<AtaqueGas>();

        da�o = aGas.da�o;
        areaDa�o = aGas.areaDa�o;
        velocidad = aGas.velocidad;

        cc.radius *= areaDa�o;
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
