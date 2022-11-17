using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    GameManager manager;
    Coroutine vidaCoro;
    SpriteRenderer sr;
    Animator anim;

    float X;
    float Y;

    [Header("Movimiento")]
    public float vel;
    public Vector2 vectMov;

    [Header("Vida")]
    public int vida;
    public Image bVida;

    private void Start()
    {
        manager = GameManager.gm;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemigo"))
        {
            Start_PierdeVida();
        }
    }

    void Start_PierdeVida()
    {
        vidaCoro ??= StartCoroutine(PerderVida());
    }

    IEnumerator PerderVida()
    {
        vida--;
        bVida.fillAmount -= .1f / 2;

        if (vida <= 0) manager.start = false;

        yield return new WaitForSeconds(.5f);
        vidaCoro = null;
    }
}