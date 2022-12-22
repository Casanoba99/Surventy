using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cartas : MonoBehaviour
{
    GameManager manager => GameManager.gm;

    public CartasSO carta;
    public GameObject prfb;
    [Space(5)]
    public TextMeshProUGUI nombre;
    public Image armaImg;
    public TextMeshProUGUI descripcion;
    public void AsignarValores()
    {
        nombre.text = carta.nombre;
        armaImg.sprite = carta.imagen;
        descripcion.text = carta.descripcion;
    }

    public void LimpiarValores()
    {
        nombre.text = "";
        armaImg.sprite = null;
        descripcion.text = "";
    }

    public void IntanciarArma()
    {
        Transform player = GameObject.Find("Player").transform;
        bool instanciar = true;

        for (int i = 0; i < player.childCount; i++)
        {
            GameObject _playerObj = player.GetChild(i).gameObject;
            if (carta.nombre == _playerObj.name)
            {
                ComprobarObjeto(_playerObj);
                instanciar = false;
                break;
            }
        }

        if (instanciar)
        {
            GameObject arma = Instantiate(prfb, player.position, Quaternion.identity, player);
            arma.name = carta.nombre;
        }

        GameObject.Find("SelectArma").SetActive(false);

        manager.EmpezarRonda();
    }

    void ComprobarObjeto(GameObject obj)
    {
        if (obj.name == "Bob the Bot")
        {
            obj.GetComponent<AtaqueOrbita>().CambiarStats();
        }
        else if (obj.name == "Gas Grenade")
        {
            //obj.GetComponent<AtaqueGas>().CambiarStats();
        }
        else if (obj.name == "Laser Sword")
        {
            obj.GetComponent<AtaqueCorte>().CambiarStats();
        }
        else if (obj.name == "Shotgun")
        {
            obj.GetComponent<AtaqueBolas>().CambiarStats();
        }
    }
}
