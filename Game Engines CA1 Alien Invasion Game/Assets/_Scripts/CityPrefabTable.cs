using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CityPrefabTable", order = 1)]
public class CityPrefabTable : ScriptableObject
{

    public GameObject[] roofModules;
    public GameObject[] bodyModules;


}
