using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemigosInput : MonoBehaviour
{
    GameManager Manager => GameManager.gm;
    AudioSource Source => GetComponent<AudioSource>();
    ParticleSystem Ps => GetComponent<ParticleSystem>();

    bool vivo = true;

    public Transform target;
    [Header("Stats")]
    public int vida;
    public float velocidad;

    private void Start()
    {

    }

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
            vida--;
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
}
