using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myPos = GetComponent<Vector3>();

        GenerateCityClusters();
    }

    CityPrefabTable myGenTable;
    [Range(0,100)]
    public int clusterChance;
    [Range(0, 100)]
    private int rng;

    Vector3 myPos;

    // Update is called once per frame
    void Update()
    {
     
        
    }

    private void GenerateCityClusters()
    {

        if (Physics.Raycast(myPos, Vector3.down, 1000))
        {

            Instantiate(myGenTable.bodyModules[1],transform.position,Quaternion.identity);

        }

        

    }
}
