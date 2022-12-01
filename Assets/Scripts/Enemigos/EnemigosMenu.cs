using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosMenu : MonoBehaviour
{
    public Transform[] puntos;
    public Transform pActual;
    [Space(5)]
    public float velocidad;

    void Start()
    {
        pActual = puntos[Random.Range(0, puntos.Length)];
    }

    void Update()
    {
        if (transform.position != pActual.position)
            transform.position = Vector3.MoveTowards(transform.position, pActual.position, velocidad * Time.deltaTime);
        else pActual = puntos[Random.Range(0, puntos.Length)];
    }
}
