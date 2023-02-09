using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Boss0Input : MonoBehaviour
{
    Coroutine dañoCoro, ataque1Coro;
    GameManager Manager => GameManager.gm;
    Animator Anims => GetComponent<Animator>();
    AudioSource Source => GetComponent<AudioSource>();
    ParticleSystem Ps => GetComponent<ParticleSystem>();

    bool vivo = false;

    public Transform target;
    [Range(0, 1)]
    public float distDaño = .5f;

    [Header("Stats")]
    public int vida;
    public int daño;
    public float velocidad;
    [Space(10)]
    public bool atacar = false;
    public GameObject ataque1Prfb;

    [Header("Ataque 1")]
    public Transform ataque1;
    public int disparos;
    public float velocidadY;
    public float velocidadX;

    void Update()
    {
        if (Manager.start)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, velocidad * Manager.tiempoDelta);

            if (!atacar) Start_Ataque1();

            if (Vector3.Distance(target.position, transform.position) < distDaño && vida > 0)
                target.GetComponent<PlayerInput>().Start_PierdeVida(daño);
        }
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
                Source.Play();
            }
        }
    }

    #region Daño
    void Start_RecibirDaño(GameObject obj)
    {
        dañoCoro ??= StartCoroutine(RecibirDaño(obj));
    }

    IEnumerator RecibirDaño(GameObject obj)
    {
        Anims.SetTrigger("Daño");
        Source.Play();
        PerderVida(obj);
        float vel = velocidad;
        velocidad /= 2;
        yield return new WaitForSeconds(.5f);
        velocidad = vel;

        dañoCoro = null;
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
    #endregion
    #region Ataque1
    void Start_Ataque1()
    {
        ataque1Coro ??= StartCoroutine(Ataque1());
    }

    IEnumerator Ataque1()
    {
        atacar = true;
        int vecesDispara = disparos;

        for (int i = 0; i < vecesDispara; i++)
        {
            GameObject atc = Instantiate(ataque1Prfb, transform.position, Quaternion.identity, transform);
            ataque1 = transform.GetChild(i);

            for (int j = 0; j < ataque1.childCount; j++)
            {
                ataque1.GetChild(j).GetComponent<Boss0Ataque1>().enabled = true;
                ataque1.GetChild(j).GetComponent<Boss0Ataque1>().velocidadY = velocidadY;
                ataque1.GetChild(j).GetComponent<Boss0Ataque1>().velocidadX = velocidadX;
                yield return new WaitForSeconds(.01f);
            }
        }

        int rot = 0;
        for (int y = 0; y < vecesDispara; y++)
        {
            ataque1 = transform.GetChild(y);
            ataque1.localRotation = Quaternion.Euler(0, 0, rot);

            for (int x = 0; x < ataque1.childCount; x++)
            {
                ataque1.GetChild(x).GetComponent<Boss0Ataque1>().disparo = true;
            }

            rot += 5;
            yield return new WaitForSeconds(.25f);
        }

        yield return new WaitForSeconds(2);

        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        atacar = false;
        ataque1Coro = null;
    }
    #endregion
}
