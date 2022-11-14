using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueCorte : MonoBehaviour
{
    GameManager manager;
    Coroutine slashCoro;

    public Transform target;
    public GameObject prefb;
    public float cooldown;
    public float radio;

    private void Start()
    {
        manager = GameManager.gm;
    }

    void Update()
    {
        if (manager.start)
        {
            Start_Slash();
        }
    }

    //---------------------------------------------------------------------------------------------

    void Start_Slash()
    {
        slashCoro ??= StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {
        yield return new WaitForSeconds(cooldown);

        // Seleccionar objetivo
        SeleccionarTarget();

        // Instanciar Ataque
        if (target) _ = Instantiate(prefb, transform.position, Quaternion.identity, transform);

        slashCoro = null;
    }

    void SeleccionarTarget()
    {
        Collider2D[] nearTargets = Physics2D.OverlapCircleAll(transform.position, radio, LayerMask.GetMask("Enemigo"));

        for (int i = 0; i < nearTargets.Length; i++)
        {
            if (Vector2.Distance(transform.position, nearTargets[i].transform.position) < (radio / 2))
            {
                target = nearTargets[i].transform;
            }
        }
    }
}
