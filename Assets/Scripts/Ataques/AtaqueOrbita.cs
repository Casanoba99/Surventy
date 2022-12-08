using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueOrbita : MonoBehaviour
{
    GameManager manager;

    public Transform bot;
    public CartasSO carta;

    [Header("Stats")]
    public int da�o;
    public float torque;

    private void Start()
    {
        manager = GameManager.gm;
        da�o = carta.da�o;
        torque = carta.velocidad;
    }

    void Update()
    {
        if (manager.start)
        {
            transform.Rotate(Vector3.forward, torque * manager.tiempoDelta);
            bot.Rotate(Vector3.forward, -torque * manager.tiempoDelta);
        }
    }
}

