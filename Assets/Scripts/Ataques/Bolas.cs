using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    GameManager manager => GameManager.gm;
    

    public float vida;
    [Header("Stats")]
    public int daño;
    public float velocidad;
    public float areaDaño;

    void Start()
    {
        AtaqueBolas ataque = GetComponentInParent<AtaqueBolas>();

        daño = ataque.daño;
        velocidad = ataque.velocidad;
        areaDaño = ataque.areaDaño;

        transform.localScale *= areaDaño;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * velocidad * transform.right);
        Destroy(gameObject, vida);
    }
}
