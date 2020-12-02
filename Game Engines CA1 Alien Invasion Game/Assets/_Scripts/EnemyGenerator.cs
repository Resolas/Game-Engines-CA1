using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GenerateShip();

    }

    [Header("Generation Settings")]
    public int frontMaxLength = 5;
    public int rearMaxLength = 5;

    public EnemyPrefabTable myGenTable;

    public float zSpacing;

    void GenerateShip()
    {
        int frontRng = Random.Range(2,frontMaxLength);
        int rearRng = Random.Range(2,rearMaxLength);

        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        // Front
        for (int i = 1; i < frontRng; i++)
        {

            if (i < frontRng - 1)
            {
                int bodyMod = Random.Range(0, myGenTable.bodyModules.Length);
                GameObject bodyBlock = Instantiate(myGenTable.bodyModules[bodyMod], new Vector3(xPos,yPos,zPos + zSpacing * -i),transform.rotation);
                bodyBlock.transform.parent = gameObject.transform;
            }
            else
            {
                int rearMod = Random.Range(0, myGenTable.rearModules.Length);
                GameObject rearBlock = Instantiate(myGenTable.rearModules[rearMod], new Vector3(xPos, yPos, zPos + zSpacing * -i), transform.rotation);
                rearBlock.transform.parent = gameObject.transform;
            }
            
        }

        // Rear
        for (int i = 1; i < rearRng; i++)
        {

            if (i < rearRng - 1)
            {
                int bodyMod = Random.Range(0, myGenTable.bodyModules.Length);
                GameObject bodyBlock = Instantiate(myGenTable.bodyModules[bodyMod], new Vector3(xPos, yPos, zPos + zSpacing * i), transform.rotation);
                bodyBlock.transform.parent = gameObject.transform;
            }
            else
            {
                int frontMod = Random.Range(0, myGenTable.bowModules.Length);
                GameObject frontBlock = Instantiate(myGenTable.bowModules[frontMod], new Vector3(xPos, yPos, zPos + zSpacing * i), transform.rotation);
                frontBlock.transform.parent = gameObject.transform;
            }

            
        }


    }


}
