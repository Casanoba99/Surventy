using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    GameManager Manager => GameManager.gm;

    public float vida;
    [Space(5)]
    [Header("Stats")]
    public int daño;
    public float areaDaño;
    public float velocidad;

    private void Start()
    {
        //Rotacion();

        AtaqueCorte Corte = transform.GetComponentInParent<AtaqueCorte>();

        daño = Corte.daño;
        areaDaño = Corte.areaDaño;
        velocidad = Corte.velocidad;

        transform.localScale *= areaDaño;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (Manager.tiempoDelta * velocidad * transform.right);
        Destroy(gameObject, vida);
    }

    void Rotacion()
    {
        Vector3 target = GetComponentInParent<AtaqueCorte>().target.position;
        Vector3 dir = target - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
