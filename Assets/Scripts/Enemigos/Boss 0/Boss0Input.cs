using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Boss0Input : MonoBehaviour
{
    Coroutine da�oCoro, ataque1Coro;
    GameManager Manager => GameManager.gm;
    Animator Anims => GetComponent<Animator>();
    AudioSource Source => GetComponent<AudioSource>();
    ParticleSystem Ps => GetComponent<ParticleSystem>();

    bool vivo = false;

    public Transform target;
    [Range(0, 1)]
    public float distDa�o = .5f;

    [Header("Stats")]
    public int vida;
    public int da�o;
    public float velocidad;
    [Space(10)]
    public bool atacar = false;
    [Header("Ataque 1")]
    public Transform ataque1;
    public float velocidadY;
    public float velocidadX;

    void Update()
    {
        if (Manager.start)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, velocidad * Manager.tiempoDelta);

            if (!atacar) Start_Ataque1();

            if (Vector3.Distance(target.position, transform.position) < distDa�o && vida > 0)
                target.GetComponent<PlayerInput>().Start_PierdeVida(da�o);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash") && vida > 0)
        {
            Start_RecibirDa�o(collision.gameObject);

            if (vida <= 0 && vivo)
            {
                vivo = false;
                velocidad = 0;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<ShadowCaster2D>().enabled = false;
                Ps.Play();
                Destroy(gameObject, 1);
                Source.Play();
            }
        }
    }

    #region Da�o
    void Start_RecibirDa�o(GameObject obj)
    {
        da�oCoro ??= StartCoroutine(RecibirDa�o(obj));
    }

    IEnumerator RecibirDa�o(GameObject obj)
    {
        Anims.SetTrigger("Da�o");
        Source.Play();
        PerderVida(obj);
        float vel = velocidad;
        velocidad /= 2;
        yield return new WaitForSeconds(.5f);
        velocidad = vel;

        da�oCoro = null;
    }

    void PerderVida(GameObject obj)
    {
        if (obj.name == "Bob")
        {
            vida -= obj.GetComponentInParent<AtaqueOrbita>().da�o;
        }
        else if (obj.name == "Gas Grenade")
        {
            vida -= obj.GetComponent<Gas>().da�o;
        }
        else if (obj.name == "Slash")
        {
            vida -= obj.GetComponent<Slash>().da�o;
        }
        else if (obj.name == "Bola")
        {
            vida -= obj.GetComponent<Bolas>().da�o;
        }
        else if (obj.name == "Shoot")
        {
            vida -= obj.GetComponent<BotTurretShoot>().da�o;
        }
    }
    #endregion
    #region Ataque1
    void Start_Ataque1()
    {
        ataque1Coro ??= StartCoroutine(Ataque1());
    }

    IEnumerator Ataque1()
    {
        for (int i = 0; i < ataque1.childCount; i++)
        {
            ataque1.GetChild(i).GetComponent<Boss0Ataque1>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }

        yield return new WaitForSeconds(3);
        atacar = false;
        ataque1Coro = null;
    }
    #endregion
}
