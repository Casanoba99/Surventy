using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cartas : MonoBehaviour
{
    GameManager manager;

    public CartasSO carta;
    public GameObject prfb;
    [Space(5)]
    public TextMeshProUGUI nombre;
    public Image armaImg;
    public TextMeshProUGUI descripcion;

    private void Start()
    {
        manager = GameManager.gm;
    }

    public void AsignarValores()
    {
        nombre.text = carta.nombre;
        armaImg.sprite = carta.imagen;
        descripcion.text = carta.descripcion;
    }

    public void IntanciarArma()
    {
        Transform player = GameObject.Find("Player").transform;

        GameObject arma = Instantiate(prfb, player.position, Quaternion.identity, player);
        arma.name = carta.nombre;

        GameObject.Find("SelectArma").SetActive(false);
        manager.start = true;
        manager.tD = true;
    }
}
