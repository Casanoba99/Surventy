using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    GameManager manager;
    Coroutine spawnCoro;

    [Header("Enemigos")]
    public GameObject[] enemigos;
    public Transform target;
    public int cantidad;
    public int hijos;
    public int min;

    bool spawn = false;
    [Header("Spawn Area")]
    public int x;
    public int y;
    public int distancia;

    private void Start()
    {
        manager = GameManager.gm;
    }

    void Update()
    {
        hijos = transform.childCount;

        if (manager.start && !spawn)
        {
            Start_Spawn();
        }

        if (manager.start && spawn && hijos < min)
        {
            Start_Spawn();
        }
    }

    #region Spawn

    void Start_Spawn()
    {
        spawnCoro ??= StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject clon = Instantiate(enemigos[0], SpawnPos(), Quaternion.identity, transform);
            clon.GetComponent<EnemigosInput>().target = target;
            Debug.Log(clon.name);

            yield return new WaitForEndOfFrame();
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
            _x = Random.Range(-x, x + 1);
            _y = Random.Range(-y, y + 1);
            pos = new Vector2(_x, _y);
            pos += target.position;
        }
        while (Vector2.Distance(target.position, pos) < distancia);

        return pos;
    }
    #endregion
}
