using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectArma : MonoBehaviour
{
    public CartasSO carta;
    public GameObject prfb;
    [Space(5)]
    public TextMeshProUGUI name;
    public Image armaImg;
    public TextMeshProUGUI descripcion;

    
    void Start()
    {
        AsignarValores();
    }

    public void AsignarValores()
    {
        name.text = carta.name;
        armaImg.sprite = carta.imagen;
        descripcion.text = carta.descripcion;
    }
}
