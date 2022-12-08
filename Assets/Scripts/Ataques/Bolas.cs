using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    readonly GameManager manager = GameManager.gm;

    public int daño;
    public float vel;

    void Start()
    {
        daño = GetComponentInParent<AtaqueBolas>().daño;
        vel = GetComponentInParent<AtaqueBolas>().velocidad;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * vel * transform.right);
    }
}
