using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnTurrets();
    }

    public Transform[] getSpawnPos;
    public GameObject[] myTurrets;

    void SpawnTurrets()
    {

        for (int i = 0; i < getSpawnPos.Length; i ++)
        {

           int turretRNG = Random.Range(0, myTurrets.Length);

             GameObject turret = Instantiate(myTurrets[turretRNG],getSpawnPos[i].position, transform.rotation);
            turret.transform.SetParent(gameObject.transform);

            
        }



    }

}
