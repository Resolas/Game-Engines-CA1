using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myPos = GetComponent<Vector3>();

        GetClusterPositions();
    }

    public CityPrefabTable myGenTable;
    [Range(0,10)]
    public int clusterNum;
    [Range(0, 100)]
    private int rng;

    Vector3 myPos;
    public int loopX = 100;
    public int loopZ = 100;

    public int sizeX = 100;
    public int sizeZ = 100;

    private float curSpawnPos;

    private void GetClusterPositions()
    {

        for (int i = 0; i < loopX; i++)
        {

            for (int j = 0; j < loopZ; j++)
            {

                if (Physics.Raycast(myPos, Vector3.down, 1000))
                {
                    var pickBlock = Random.Range(0,myGenTable.bodyModules.Length);

                    GenerateCityCluster();

                    Instantiate(myGenTable.bodyModules[pickBlock], transform.position, Quaternion.identity);

                }


            }

        }   

    }

    private void GenerateCityCluster()
    {

        for (int i = 0; i < sizeX; i++)
        {

            for (int j = 0; j < sizeZ; j++)
            {

            }
        }

    }


}
