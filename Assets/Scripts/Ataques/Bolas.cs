using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    readonly GameManager manager = GameManager.gm;

    public int da�o;
    public float vel;

    void Start()
    {
        da�o = GetComponentInParent<AtaqueBolas>().da�o;
        vel = GetComponentInParent<AtaqueBolas>().velocidad;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * vel * transform.right);
    }
}
