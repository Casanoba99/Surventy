using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0Ataque1 : MonoBehaviour
{
    public float velocidadY;
    public float velocidadX;
    public bool disparo = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        if (GameManager.gm.start && disparo)
        {
            transform.parent = null;
            transform.position += transform.up * velocidadY * GameManager.gm.tiempoDelta;
            transform.position += transform.right * velocidadX * GameManager.gm.tiempoDelta;

            Destroy(gameObject, 3);
        }
    }
}
