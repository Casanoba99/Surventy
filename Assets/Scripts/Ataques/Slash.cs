using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    readonly GameManager manager = GameManager.gm;

    public float vida;
    [Space(5)]
    public float daño;
    public float vel;

    private void Start()
    {
        Rotacion();

        daño = transform.GetComponent<AtaqueCorte>().daño;
        vel = transform.GetComponent<AtaqueCorte>().velocidad;

        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * vel * transform.right);
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
