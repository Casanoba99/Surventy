using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    readonly GameManager manager = GameManager.gm;

    public float vel;
    public float vida;

    private void Start()
    {
        Rotacion();
        transform.parent = null;
    }

    void Update()
    {
        transform.position = transform.position + (manager.tiempoDelta * vel * transform.right);
        Destroy(gameObject, vida);
    }

    void Rotacion()
    {
        Debug.Log(GetComponentInParent<AtaqueCorte>().name);
        Vector3 target = GetComponentInParent<AtaqueCorte>().target.position;
        Vector3 dir = target - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
