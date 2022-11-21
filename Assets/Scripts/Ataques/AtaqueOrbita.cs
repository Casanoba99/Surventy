using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueOrbita : MonoBehaviour
{
    GameManager manager;

    public Transform bot;
    public float torque;

    private void Start()
    {
        manager = GameManager.gm;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, torque * manager.tiempoDelta);

        bot.Rotate(Vector3.forward, -torque * manager.tiempoDelta);
    }
}
