using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{   // SCOPE - Creates the Individual Building


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
            float yPos = transform.position.y;

            int pickBodyModule = Random.Range(0,myBuilding.bodyModules.Length);
            int pickRoofModule = Random.Range(0,myBuilding.roofModules.Length);

            if (i < heightRng - 1   && myBuilding.bodyModules != null)
            {
                Instantiate(myBuilding.bodyModules[pickBodyModule], new Vector3(xPos,yPos + (ySpacing * i), zPos), Quaternion.identity);
            }
            else if (myBuilding.roofModules != null)
            {
                Instantiate(myBuilding.roofModules[pickRoofModule], new Vector3(xPos,yPos + (ySpacing * i),zPos),Quaternion.identity);
            }

            

        }


    }
}
