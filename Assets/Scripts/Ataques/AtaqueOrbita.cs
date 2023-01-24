using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueOrbita : MonoBehaviour
{

    GameManager Manager => GameManager.gm;

    public CartasSO carta;

    [Header("Stats")]
    public int daño;
    public float areaDaño;
    public float radioAtaque;
    public float velocidadRot;

    private void Start()
    {
        CambiarStats();
    }

    void Update()
    {
        if (Manager.start)
        {
            transform.Rotate(Vector3.forward, velocidadRot * Manager.tiempoDelta);
        }
        else
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void CambiarStats()
    {
        CheckNivel check = GetComponent<CheckNivel>();

        daño = check.daño;
        areaDaño = check.areaDaño;
        radioAtaque = check.radioAtaque;
        velocidadRot = check.velocidad;

        if (check.nivel == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (check.nivel == 2)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (check.nivel == 3)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (check.nivel == 4)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }

        // Aumentar ladistancia desde el centro
        for (int i = 0; i < check.nivel; i++)
        {
            Transform child = transform.GetChild(i).transform;
            // Eje X
            if (child.GetSiblingIndex() <= 1)
            {
                child.GetComponent<Bob>().CambiarRadio(new Vector3(radioAtaque, 0, 0));
            }
            // Eje Y
            else if (child.GetSiblingIndex() >= 2)
            {
                child.GetComponent<Bob>().CambiarRadio(new Vector3(0, radioAtaque, 0));

            }

            // Aumentra tamaño
            child.localScale = Vector3.one * areaDaño;
        }
    }
}

