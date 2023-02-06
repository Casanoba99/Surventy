using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss0 : MonoBehaviour
{
    public SpawnEnemigos padre;
    public GameObject prefb;
    public float tSpawn = .1f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(tSpawn);

        GameObject clon = Instantiate(prefb, transform.position, Quaternion.identity, padre.transform);
        clon.name = "Boss";
        clon.GetComponent<Boss0Input>().target = padre.target;
        Destroy(gameObject);
    }
}
