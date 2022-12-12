using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    GameManager manager => GameManager.gm;

    public float vida;
    public int da�o;
    public float vel;

    void Start()
    {
        da�o = GetComponentInParent<AtaqueBolas>().da�o;
        vel = GetComponentInParent<AtaqueBolas>().velocidad;
        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * vel * transform.right);
        Destroy(gameObject, vida);
    }
}
