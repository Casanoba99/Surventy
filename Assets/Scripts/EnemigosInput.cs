using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosInput : MonoBehaviour
{
    readonly GameManager manager = GameManager.gm;

    public Transform target;
    public float velocidad;

    void Update()
    {
        if (manager.start) transform.position = Vector3.MoveTowards(transform.position, target.position, velocidad * manager.tiempoDelta);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash"))
        {
            Destroy(gameObject);
        }
    }
}
