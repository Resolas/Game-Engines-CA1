using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnShips());
        StartCoroutine(SpawnFlyers());
        spawnBoss = StartCoroutine(SpawnBoss());

    }
    [Header("Unit Spawn Settings")]
    public GameObject myShipCore;
    public GameObject[] myFlyers;
    public GameObject myBossCore;       

    public Transform[] mySpawnPos = new Transform[3];

    public bool disableShip,disableFlyer,disableBoss;

    [Header("General Settings")]

    public bool waveMode = false;
    public int maxWave = 5;
    private int curWave = 1;



    public int spawnShipNum = 1;
    public int spawnFlyerNum = 10;

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnShips()
    {
        while (true)
        {
            if (disableShip != true)
            {
                for (int i = 0; i < spawnShipNum; i++)
                {
                    float randX = Random.Range(-200, 200);
                    float randY = Random.Range(0, 1000);
                    float randZ = Random.Range(-200, 200);

                    Instantiate(myShipCore, mySpawnPos[0].position + new Vector3(randX, randY, randZ), transform.rotation);
                }
            }
            
            

            yield return new WaitForSeconds(10);
        }

    }

    IEnumerator SpawnFlyers()
    {
        while (true)
        {
            if (disableFlyer != true)
            {
                for (int i = 0; i < spawnFlyerNum; i++)
                {

                    for (int j = 0; j < myFlyers.Length; j++)
                    {
                        float randX = Random.Range(-400, 400);
                        float randY = Random.Range(0, 400);
                        float randZ = Random.Range(-200, 200);

                        Instantiate(myFlyers[j], mySpawnPos[1].position + new Vector3(randX, randY, randZ), transform.rotation);
                    }

                }
            }

            
            yield return new WaitForSeconds(30);
        }

        

    }

    Coroutine spawnBoss;

    IEnumerator SpawnBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);

            Instantiate(myBossCore, mySpawnPos[2].position, Quaternion.identity);

            StopCoroutine(spawnBoss);

            /*
            if (disableBoss != true)
            {
                if (curWave == maxWave)
                {
                    if (myBossCore != null)
                    {
                        Instantiate(myBossCore, mySpawnPos[2].position, Quaternion.identity);
                    }
                }
            }
            */

            

        }

    }

}
