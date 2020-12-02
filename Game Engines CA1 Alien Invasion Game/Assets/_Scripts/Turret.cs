using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkForTargets());
    }

    [Header("Settings")]

    [SerializeField]
    private Transform myTarget;
    public FiringSys[] myWeapons;
    public float range = 250f;
    private float closest;
    public bool isFriendly = false;
    public float getDist;


    // Update is called once per frame
    void FixedUpdate()
    {

     //   TrackTarget();

    }


    [Header("Targeting Settings")]
    public bool findEnemy = false;
    public bool findFriendly = true;
    public bool findPlayer = true;
    public bool findStructure = false;

    public float trackRange = 100f;

    private Transform closestTarget;


    void TrackTarget()
    {

        Collider[] targets = Physics.OverlapSphere(transform.position,trackRange);

        foreach (Collider target in targets)
        {
            if (target.CompareTag("Untagged")) continue;    // Ignore everything that's Untagged
            if (target.CompareTag("Enemy") && findEnemy != true) continue;
            if (target.CompareTag("Friendly") && findFriendly != true) continue;
            if (target.CompareTag("Player") && findPlayer != true) continue;
            if (target.CompareTag("Structure") && findStructure != true) continue;


            Vector3 pointAB = target.transform.position - transform.position;
                getDist = pointAB.sqrMagnitude;

                Debug.Log("TrackTest");
                Debug.DrawLine(target.transform.position, transform.position);



                myTarget = closestTarget;   // Finalized Target

            
            
                
            

            

        }

        

    }

    IEnumerator checkForTargets()
    {
        while (true)
        {
            TrackTarget();
            yield return new WaitForSeconds(0.1f);
        }
       
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
