using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    GameManager Manager => GameManager.gm;

    public float vida;
    [Space(5)]
    [Header("Stats")]
    public float da�o;
    public float areaDa�o;
    public float velocidad;

    private void Start()
    {
        Rotacion();

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

    void Rotacion()
    {
        Vector3 target = GetComponentInParent<AtaqueCorte>().target.position;
        Vector3 dir = target - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
