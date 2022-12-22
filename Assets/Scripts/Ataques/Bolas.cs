using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    GameManager manager => GameManager.gm;
    

    public float vida;
    [Header("Stats")]
    public int da�o;
    public float velocidad;
    public float areaDa�o;

    void Start()
    {
        AtaqueBolas ataque = GetComponentInParent<AtaqueBolas>();

        da�o = ataque.da�o;
        velocidad = ataque.velocidad;
        areaDa�o = ataque.areaDa�o;

        transform.localScale *= areaDa�o;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * velocidad * transform.right);
        Destroy(gameObject, vida);
    }
}
