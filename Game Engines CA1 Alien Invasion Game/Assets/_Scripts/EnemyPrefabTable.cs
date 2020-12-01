using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyPrefabTable",order = 2)]
public class EnemyPrefabTable : ScriptableObject
{

    public GameObject[] rearModules;
    public GameObject[] bodyModules;
    public GameObject[] bowModules;



}
