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
    bool spawnBoss = false;

    public Transform target;

    [Header("Enemigos")]
    public GameObject[] enemigos;
    public int cantidad = 10;
    public int min = 5;

    [Header("Jefes")]
    public GameObject[] jefes;

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
        if (Manager.ronda == 10 && !spawnBoss)
        {
            int bossN = UnityEngine.Random.Range(0, jefes.Length);
            GameObject clon = Instantiate(jefes[bossN], SpawnPos(), Quaternion.identity);
            if (bossN == 0) clon.GetComponent<SpawnBoss0>().padre = this;

            spawnBoss = true;
        }
        
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

    public void PararSpawn()
    {
        if (spawnCoro != null)
        {
            StopCoroutine(spawnCoro);
            spawnCoro = null;
        }
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
        int max = 11;
        int r = UnityEngine.Random.Range(0, max);
        if (r > 7 && r < 10) return 1;
        else if (r > 9 && r < max) return 2;

        return 0;
    }
    #endregion

    public void EliminarEnemigos()
    {
        Debug.Log(hijos);
        int n = 0;
        for (int i = hijos - 1; i >= 0; i--)
        {
            n++;
            Destroy(transform.GetChild(i).gameObject);
        }
        Debug.Log(n);
    }
}
