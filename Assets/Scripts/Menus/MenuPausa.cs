using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    GameManager manager;

    private void Start()
    {
        manager = GameManager.gm;
    }

    public void Reanudar()
    {
        manager.start = !manager.start;
        manager.tD = !manager.tD;
        gameObject.SetActive(false);
    }
}
