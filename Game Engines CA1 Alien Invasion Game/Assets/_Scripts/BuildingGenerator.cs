using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        heightRng = Random.Range(minHeight,maxHeight); 

        GenerateBuilding();
    }

    [Header("Building Generation Settings")]

    public CityPrefabTable myBuilding;
    public int minHeight = 1;
    public int maxHeight = 10;

    private int heightRng;

    

    public float ySpacing = 20;

    void GenerateBuilding()
    {

        for (int i = 0; i < heightRng; i++)
        {
            float xPos = transform.position.x;
            float zPos = transform.position.z;

            int pickModule = Random.Range(0,myBuilding.bodyModules.Length);

            Instantiate(myBuilding.bodyModules[pickModule], new Vector3(xPos,ySpacing*i,zPos), Quaternion.identity);

        }


    }
}
