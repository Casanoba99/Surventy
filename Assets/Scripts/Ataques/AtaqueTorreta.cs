using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueTorreta : MonoBehaviour
{

    GameManager Manager => GameManager.gm;

    int proyectiles = 0;
    bool instanciados = false;

    [HideInInspector]
    public int duracion = 0;

    public GameObject prefb;
    public CartasSO carta;

    [Header("Stats")]
    public int daño;
    public float cooldown;
    public float areaDaño;
    public float radioAtaque;
    public float velocidad;

    private void Start()
    {
        CambiarStats();
    }

    void Update()
    {
        if (Manager.start && !instanciados)
        {
            // Instanciar ataque
            InstanciarTorretas();
        }

        if (!Manager.start && !Manager.tD) instanciados = false;
    }

    //---------------------------------------------------------------------------------------------

    void InstanciarTorretas()
    {
        for (int i = 0; i < proyectiles; i++)
        {
            Vector3 pos;
            int x = Random.Range(-12, 13);
            int y = Random.Range(-6, 7);
            pos = new Vector3(x, y, 0);

            GameObject bot = Instantiate(prefb, pos, Quaternion.identity, transform);
            bot.name = carta.nombre;
        }

        instanciados = true;
    }

    public void CambiarStats()
    {
        CheckNivel check = GetComponent<CheckNivel>();

        daño = check.daño;
        cooldown = check.cooldown;
        areaDaño = check.areaDaño;
        radioAtaque = check.radioAtaque;
        velocidad = check.velocidad;

        if (check.nivel == 1)
        {
            proyectiles = 1;
        }
        else if (check.nivel == 2)
        {
            proyectiles = 2;
        }
        else if (check.nivel == 3)
        {
            proyectiles = 3;
        }
        else if (check.nivel == 4)
        {
            proyectiles = 4;
        }
    }
}
