using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("Settings")]

    [SerializeField]
    private GameObject myTarget;
    public FiringSys[] myWeapons;
    public float range = 250f;
    private float closest;
    public bool isFriendly = false;
    public float getDist;
    

    // Update is called once per frame
    void FixedUpdate()
    {

        TrackTarget();

    }

    void TrackTarget()
    {

        Vector3 pointAB = myTarget.transform.position - transform.position;
        getDist = pointAB.sqrMagnitude;

    }


    void Shoot()
    {

        for (int i = 0; i < myWeapons.Length; i++)
        {
            myWeapons[i].FireWeapons();

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,range);

    }

}
