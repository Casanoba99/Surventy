using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carta Arma")]
public class CartasSO : ScriptableObject
{
    public string nombre;
    public Sprite imagen;
    [TextArea]
    public string descripcion;
    [Header("Stats Base")]
    public int da�o;
    public float cooldown;
    public float areaDa�o;
    public float radioAtaque;
    public float velocidad;
}
