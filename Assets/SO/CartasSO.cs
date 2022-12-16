using System;
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
    public StatsNiveles[] nivel;
}

[Serializable]
public class StatsNiveles
{
    public int daño;
    public float cooldown;
    public float areaDaño;
    public float radioAtaque;
    public float velocidad;
}
