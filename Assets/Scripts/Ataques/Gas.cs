using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Gas : MonoBehaviour
{
    GameManager manager => GameManager.gm;
    ParticleSystem ps => GetComponent<ParticleSystem>();
    AtaqueGas aGas => GetComponentInParent<AtaqueGas>();

    Vector3 target;


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

        if (transform.position == target)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            ps.Play();
        }
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
