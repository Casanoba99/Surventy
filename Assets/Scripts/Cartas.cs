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

        // Comprueba todos los hijos del jugador
        for (int i = 0; i < player.childCount; i++)
        {
            GameObject _playerObj = player.GetChild(i).gameObject;
            // Comprueba sí ya la tiene
            if (carta.nombre == _playerObj.name)
            {
                instanciar = false;
                ComprobarObjeto(_playerObj);
                break;
            }
        }

        if (instanciar)
        {
            GameObject arma = Instantiate(prfb, player.position, Quaternion.identity, player);
            arma.name = carta.nombre;
            arma.GetComponent<CheckNivel>().SubirNivel();
        }

        GameObject.Find("SelectArma").SetActive(false);

        manager.EmpezarRonda();
    }

    void ComprobarObjeto(GameObject obj)
    {
        obj.GetComponent<CheckNivel>().SubirNivel();
        if (obj.GetComponent<CheckNivel>().nivel == 4) BorrarDeLista(obj);
    }

    void BorrarDeLista(GameObject obj)
    {
        SelectArma sArma = GetComponentInParent<SelectArma>();

        for (int i = 0; i < sArma.cartaPrfb.Count; i++)
        {
            if (obj.name == sArma.cartaPrfb[i].carta.nombre) sArma.cartaPrfb.Remove(sArma.cartaPrfb[i]);
        }
    }
}
