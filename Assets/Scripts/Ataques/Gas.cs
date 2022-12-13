using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Gas : MonoBehaviour
{
    GameManager manager => GameManager.gm;
    ParticleSystem ps => GetComponent<ParticleSystem>();
    CircleCollider2D cc => GetComponent<CircleCollider2D>();
    AtaqueGas aGas => GetComponentInParent<AtaqueGas>();

    Vector3 target;
    bool explota = false;

    [Header("Stats")]
    public int daño;
    public float velocidad;

    void Start()
    {
        target = aGas.target.position;
        daño = aGas.daño;
        velocidad = aGas.velocidad;
        transform.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, velocidad * manager.tiempoDelta);

        if (transform.position == target && !explota)
        {
            explota = true;
            GetComponent<SpriteRenderer>().enabled = false;
            cc.enabled = true;
            ps.Play();
            Destroy(gameObject, ps.main.duration + .25f);
        }
    }
}
