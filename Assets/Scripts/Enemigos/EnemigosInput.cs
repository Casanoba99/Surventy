using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemigosInput : MonoBehaviour
{
    readonly GameManager manager = GameManager.gm;
    AudioSource source;
    
    public Transform target;
    public float velocidad;

    private void Start()
    {
        source= GetComponent<AudioSource>();
    }

    void Update()
    {
        if (manager.start) 
            transform.position = Vector3.MoveTowards(transform.position, target.position, velocidad * manager.tiempoDelta);

        if (Vector3.Distance(target.position, transform.position) < .2f)
            target.GetComponent<PlayerInput>().Start_PierdeVida();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash"))
        {
            source.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<ShadowCaster2D>().enabled = false;
            Destroy(gameObject, 1);
        }
    }
}
