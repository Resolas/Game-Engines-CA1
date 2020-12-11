using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFlyer());
    }

    public GameObject[] myFlyers;


    IEnumerator SpawnFlyer()
    {

        while (true)
        {

            int unitRNG = Random.Range(0,myFlyers.Length);

            Instantiate(myFlyers[unitRNG],transform.position,transform.rotation);

            yield return new WaitForSeconds(20);
        }

    }
    
}
