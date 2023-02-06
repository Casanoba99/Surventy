using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0Ataque1 : MonoBehaviour
{
    public float velocidadY;
    public float velocidadX;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        if (GameManager.gm.start)
        {
            transform.localPosition += new Vector3(velocidadX, velocidadY, 0) * GameManager.gm.tiempoDelta;
        }
    }
}
