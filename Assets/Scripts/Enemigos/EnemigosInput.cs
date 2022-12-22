using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemigosInput : MonoBehaviour
{
    Coroutine velCoro;
    GameManager Manager => GameManager.gm;
    Animator anims => GetComponent<Animator>();
    AudioSource Source => GetComponent<AudioSource>();
    ParticleSystem Ps => GetComponent<ParticleSystem>();

    bool vivo = true;

    public Transform target;
    [Header("Stats")]
    public int vida;
    public float velocidad;

    void Update()
    {
        if (Manager.start)
            transform.position = Vector3.MoveTowards(transform.position, target.position, velocidad * Manager.tiempoDelta);

        if (Vector3.Distance(target.position, transform.position) < .2f && vida > 0)
            target.GetComponent<PlayerInput>().Start_PierdeVida();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash") && vida > 0)
        {
            anims.SetTrigger("Daño");
            PerderVida(collision.gameObject);
            Start_PausaVelocidad();

            if (vida <= 0 && vivo)
            {
                vivo = false;
                velocidad = 0;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<ShadowCaster2D>().enabled = false;
                Ps.Play();
                Destroy(gameObject, 1);
            }
            Source.Play();
        }
    }

    void PerderVida(GameObject obj)
    {
        if (obj.name == "Bob")
        {
            vida -= obj.GetComponentInParent<AtaqueOrbita>().daño;
        }
        else if (obj.name == "Gas Grenade")
        {
            //obj.GetComponent<AtaqueGas>().CambiarStats();
        }
        else if (obj.name == "Slash")
        {
            vida -= obj.GetComponent<Slash>().daño;
        }
        else if (obj.name == "Bola")
        {
            vida -= obj.GetComponent<Bolas>().daño;
        }
    }

    void Start_PausaVelocidad()
    {
        velCoro ??= StartCoroutine(PausaVelocidad());
    }

    IEnumerator PausaVelocidad()
    {
        float vel = velocidad;
        velocidad = 0;
        yield return new WaitForSeconds(.5f);
        velocidad = vel;

        velCoro = null;
    }
}
