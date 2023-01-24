using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemigos : MonoBehaviour
{
    GameManager Manager => GameManager.gm;
    Coroutine spawnCoro;

    int hijos;
    bool spawn = false;

    [Header("Enemigos")]
    public GameObject[] enemigos;
    public Transform target;
    public int cantidad = 10;
    public int min = 5;

    [Header("Spawn Area")]
    public Tilemap map;
    public int distancia;
    public float tiempoSpawn = .2f;

    void Update()
    {
        hijos = transform.childCount;

        if (Manager.start && !spawn)
        {
            Start_SpawnPoint();
        }

        if (Manager.start && spawn && hijos < min)
        {
            Start_SpawnPoint();
        }
    }

    #region Spawn
    void Start_SpawnPoint()
    {
        spawnCoro ??= StartCoroutine(SpawnPoint());
    }

    IEnumerator SpawnPoint()
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject clon = Instantiate(enemigos[EnemyType()], SpawnPos(), Quaternion.identity);
            clon.GetComponent<SpawnPoint>().id = i;
            clon.GetComponent<SpawnPoint>().padre = this;

            yield return new WaitForSeconds(tiempoSpawn);
        }
        spawn = true;
        spawnCoro = null;
    }

    Vector2 SpawnPos()
    {
        Vector3 pos;
        int _x, _y;

        do
        {
            _x = UnityEngine.Random.Range(-(map.size.x/2), map.size.x / 2);
            _y = UnityEngine.Random.Range(-(map.size.y / 2), map.size.y / 2);
            pos = new Vector2(_x, _y);
        }
        while (Vector2.Distance(target.position, pos) < distancia);

        return pos;
    }

    int EnemyType()
    {
        int r = UnityEngine.Random.Range(0, 10);
        if (r > 5 && r < 8) return 1;
        else if (r > 7 && r < 10) return 2;

        return 0;
    }
    #endregion

    public void EliminarEnemigos()
    {
        for (int i = hijos - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
