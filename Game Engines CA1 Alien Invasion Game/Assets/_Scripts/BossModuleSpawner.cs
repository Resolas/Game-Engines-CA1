using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModuleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnModules());
    }

    public int spawnCount = 20;
    public BossPrefabTable myPrefabTable;
    public Transform parentToRing;
    

    IEnumerator SpawnModules()
    {

        while (true)
        {

            for (int i = 0; i < spawnCount; i ++)
            {
                int rng = Random.Range(0,myPrefabTable.myModules.Length);
                GameObject mySpawnedMod = Instantiate(myPrefabTable.myModules[rng],transform.position,transform.rotation);
                mySpawnedMod.transform.SetParent(parentToRing);

                yield return new WaitForSeconds(1);
            }


            yield return new WaitForSeconds(60);
        }


    }

}
