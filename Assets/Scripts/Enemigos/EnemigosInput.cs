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

        if (Vector3.Distance(target.position, transform.position) < .4f && vida > 0)
            target.GetComponent<PlayerInput>().Start_PierdeVida();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash") && vida > 0)
        {
            Start_RecibirDaño(collision.gameObject);

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


    void Start_RecibirDaño(GameObject obj)
    {
        velCoro ??= StartCoroutine(RecibirDaño(obj));
    }

    IEnumerator RecibirDaño(GameObject obj)
    {
        anims.SetTrigger("Daño");
        PerderVida(obj);
        float vel = velocidad;
        velocidad /= 2;
        yield return new WaitForSeconds(.5f);
        velocidad = vel;

        velCoro = null;
    }
    void PerderVida(GameObject obj)
    {
        if (obj.name == "Bob")
        {
            vida -= obj.GetComponentInParent<AtaqueOrbita>().daño;
        }
        else if (obj.name == "Gas Grenade")
        {
            vida -= obj.GetComponent<Gas>().daño;
        }
        else if (obj.name == "Slash")
        {
            vida -= obj.GetComponent<Slash>().daño;
        }
        else if (obj.name == "Bola")
        {
            vida -= obj.GetComponent<Bolas>().daño;
        }
        else if (obj.name == "Shoot")
        {
            vida -= obj.GetComponent<BotTurretShoot>().daño;
        }
    }
}
