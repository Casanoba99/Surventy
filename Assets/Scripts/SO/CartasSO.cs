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
}
