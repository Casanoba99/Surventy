using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    GameManager manager;
    Coroutine dañoCoro, recuperarCoro;
    SpriteRenderer sr;
    AudioSource source;
    Animator anim;

    float X;
    float Y;
    float rTiempo;

    public VictoriaDerrota mVD;

    [Header("Movimiento")]
    public float vel;
    public Vector2 vectMov;

    [Header("Vida")]
    public int vida;
    public Image bVida;
    [Space(5)]
    public float recuTiempo = 10f;

    private void Start()
    {
        manager = GameManager.gm;
        source = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movimiento();

        if (vida < 10)
        {
            if (rTiempo >= recuTiempo)
            {
                Start_RecuperarVida();
            }
            else
            {
                rTiempo += manager.tiempoDelta;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            int daño = collision.GetComponent<Boss0Ataque1>().daño;
            Start_PierdeVida(daño);
        }
    }

    void Movimiento()
    {
        X = Input.GetAxisRaw("Horizontal");
        Y = Input.GetAxisRaw("Vertical");
        vectMov = new Vector2(X, Y);

        if (manager.start)
        {
            transform.position = (Vector2)transform.position + (manager.tiempoDelta * vel * vectMov);

            if (vectMov != Vector2.zero)
            {
                if (vectMov.x > 0) sr.flipX = false;
                else sr.flipX = true;

                anim.SetBool("Move", true);
            }
            else anim.SetBool("Move", false);
        }
    }

    public void Start_PierdeVida(int daño)
    {
        dañoCoro ??= StartCoroutine(PerderVida(daño));
    }

    IEnumerator PerderVida(int daño)
    {
        rTiempo = 0;
        StopCoroutine(RecuperarVida());
        recuperarCoro = null;

        anim.SetTrigger("Daño");
        vida -= daño;
        bVida.fillAmount -= (float)daño / 10;
        source.Play();

        if (vida <= 0)
        {
            manager.start = false;
            mVD.gameObject.SetActive(true);
            mVD.Resolucion(false);
        }

        yield return new WaitForSeconds(1f);
        dañoCoro = null;
    }

    void Start_RecuperarVida()
    {
        recuperarCoro ??= StartCoroutine(RecuperarVida());
    }

    IEnumerator RecuperarVida()
    {
        while (vida < 10)
        {
            vida++;
            bVida.fillAmount += (float)1 / 10;
            yield return new WaitForSeconds(1f);
        }

        rTiempo = 0;
        recuperarCoro = null;
    }
}
