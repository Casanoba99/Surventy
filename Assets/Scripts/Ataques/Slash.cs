using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    GameManager Manager => GameManager.gm;

    public float vida;
    [Space(5)]
    [Header("Stats")]
    public int da�o;
    public float areaDa�o;
    public float velocidad;

    private void Start()
    {
        //Rotacion();

        AtaqueCorte Corte = transform.GetComponentInParent<AtaqueCorte>();

        da�o = Corte.da�o;
        areaDa�o = Corte.areaDa�o;
        velocidad = Corte.velocidad;

        transform.localScale *= areaDa�o;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (Manager.tiempoDelta * velocidad * transform.right);
        Destroy(gameObject, vida);
    }
}
