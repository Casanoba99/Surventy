using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTurretShoot : MonoBehaviour
{
    GameManager Manager => GameManager.gm;
    Animator Anims => GetComponent<Animator>();

    public bool explota = false;

    public float vida;

    [Header("Stats")]
    public int daño;
    public float areaDaño;
    public float velocidad;

    void Start()
    {
        Rotacion();
        BotTorreta bot = transform.GetComponentInParent<BotTorreta>();

        daño = bot.daño;
        areaDaño = bot.areaDaño;
        velocidad = bot.velocidad;

        transform.localScale *= areaDaño;

        transform.parent = null;
    }

    void Update()
    {
        transform.position += (Manager.tiempoDelta * velocidad * transform.right);
        Destroy(gameObject, vida);
    }

    void Rotacion()
    {
        Vector3 target = GetComponentInParent<BotTorreta>().target.position;
        Vector3 dir = target - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo") && !explota)
        {
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        explota = true;
        velocidad = 0;
        GetComponent<CircleCollider2D>().radius *= 2;
        Anims.SetTrigger("Explosion");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
