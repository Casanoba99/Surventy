using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    GameManager manager;
    Coroutine vidaCoro;
    SpriteRenderer sr;
    AudioSource source;
    Animator anim;

    float X;
    float Y;

    public VictoriaDerrota mVD;

    [Header("Movimiento")]
    public float vel;
    public Vector2 vectMov;

    [Header("Vida")]
    public int vida;
    public Image bVida;

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

    public void Start_PierdeVida(int da�o)
    {
        vidaCoro ??= StartCoroutine(PerderVida(da�o));
    }

    IEnumerator PerderVida(int da�o)
    {
        anim.SetTrigger("Da�o");
        vida -= da�o;
        bVida.fillAmount -= (float)da�o / 10;
        source.Play();

        if (vida <= 0)
        {
            manager.start = false;
            mVD.gameObject.SetActive(true);
            mVD.Resolucion(false);
        }

        yield return new WaitForSeconds(1f);
        vidaCoro = null;
    }
}
