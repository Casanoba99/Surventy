using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    GameManager manager;
    SpriteRenderer sr;
    Animator anim;

    float X;
    float Y;

    [Header("Movimiento")]
    public float vel;
    public Vector2 vectMov;

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

            transform.position = (Vector2)transform.position + (manager.tiempoDelta * vel * vectMov);
        if (manager.start)
        {
            if (vectMov != Vector2.zero)
            {
                if (vectMov.x > 0) sr.flipX = false;
                else sr.flipX = true;

                anim.SetBool("Move", true);
            }
            else anim.SetBool("Move", false);

        }
    }
}
