using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    GameManager Manager => GameManager.gm;
    public float VelocidadRot => GetComponentInParent<AtaqueOrbita>().velocidadRot;
    public int lado = 1;

    private void Start()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Update()
    {
        if (Manager.start)
        {
            transform.Rotate(Vector3.forward, -VelocidadRot * Manager.tiempoDelta);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void CambiarRadio(Vector3 radio)
    {
        transform.localPosition = radio * lado;
    }
}
