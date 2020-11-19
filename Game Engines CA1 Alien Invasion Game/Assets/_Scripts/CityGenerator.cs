using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateCityClusters();
    }

    CityPrefabTable myGenTable;
    [Range(0,100)]
    public int clusterChance;
    [Range(0, 100)]
    private int rng;



    // Update is called once per frame
    void Update()
    {
     
        
    }

    private void GenerateCityClusters()
    {




    }
}
