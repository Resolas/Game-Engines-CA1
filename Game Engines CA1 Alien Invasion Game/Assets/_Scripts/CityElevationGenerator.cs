using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityElevationGenerator : MonoBehaviour
{   // SCOPE - used to generate the entire level (same as city gen but has no rng to spawn and raycast)

    [Header("Generation Settings")]
    public int loopX = 3;
    public int loopZ = 3;
    public float baseX = 100, baseZ = 100;
    public GameObject sectionGenerator;


    private void Start()
    {
        GenerateCitySection();
    }

    void GenerateCitySection()
    {

        for (int i = 0; i < loopX; i++)
        {

            for (int j = 0; j < loopZ; j++)
            {
                float xPos = transform.position.x + (i * baseX);
                float zPos = transform.position.z + (j * baseZ);


                GameObject mySection = Instantiate(sectionGenerator,new Vector3(xPos,0,zPos),Quaternion.identity);

            }

        }

    }

    private void OnDrawGizmos()
    {
        float sizeX = baseX * loopX;
        float sizeZ = baseZ * loopZ;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(baseX * loopX / 2, 0, baseZ * loopZ / 2), new Vector3(sizeX, 500, sizeZ));
    }

}
