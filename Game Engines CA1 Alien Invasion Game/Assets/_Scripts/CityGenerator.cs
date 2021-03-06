﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{   // SCOPE - used to generate the section or district the CityElevationGenerator Creates

    // Start is called before the first frame update
    void Start()
    {
    //    myPos = GetComponent<Vector3>();

        GenerateBuildingPos();
    }

//    public CityPrefabTable myGenTable;
    [Header("Spawn Settings")]
    public GameObject buildingType;
//    [Range(0,10)]
//    public int clusterNum;
    [Range(0, 100)]
    public int spawnChance;
    public bool rngOn;
    private int rngVal;

    [Header("Number Of Objects & Spacing")]
    Vector3 myPos;
    public int loopX = 100;
    public int loopZ = 100;

    public int baseX = 100;
    public int baseZ = 100;

    private float curSpawnPos;

    private void GenerateBuildingPos()
    {

        for (int i = 0; i < loopX; i++)
        {

            for (int j = 0; j < loopZ; j++)
            {

                if (rngOn) rngVal = Random.Range(0,100);

                if (rngVal < spawnChance || rngOn != true)
                {
                    float xPos = transform.position.x + (baseX * i);
                    float zPos = transform.position.z + (baseZ * j);

                    Vector3 rayPos = new Vector3(xPos, 500, zPos);
                    LayerMask mask = LayerMask.GetMask("Ground");

                    RaycastHit hit;

                    Debug.Log("SPAWN");
                    if (Physics.Raycast(rayPos, Vector3.down, out hit, 1000f, ~mask))
                    {
                        //   var pickBlock = Random.Range(0,myGenTable.bodyModules.Length);



                        //    GenerateCityCluster();

                        Instantiate(buildingType, new Vector3(xPos, hit.point.y, zPos), Quaternion.identity);

                    }

                }

                


            }

        }   

    }

    private void GenerateCityCluster()
    {
        /*
        for (int i = 0; i < sizeX; i++)
        {

            for (int j = 0; j < sizeZ; j++)
            {

            }
        }
        */
    }

    private void OnDrawGizmos()
    {
        float sizeX = baseX * loopX;
        float sizeZ = baseZ * loopZ;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(baseX * loopX/2, 0, baseZ * loopZ/2), new Vector3(sizeX,500,sizeZ));
    }


}
