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

   // [SerializeField]
    public Transform myTarget;
    public FiringSys[] myWeapons;
    public float range = 250f;
    private float closest;
    public bool isFriendly = false;
    public float getDistToTarget;


    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.DrawLine(myTarget.transform.position, transform.position, Color.red);
        transform.LookAt(myTarget);

        if (myTarget != null)
        {
            for (int i = 0; i < myWeapons.Length; i++)  // Fire Weapon Systems
            {
                myWeapons[i].FireWeapons();
                Debug.Log("Fire Laser");
            }
        }

    }


    [Header("Targeting Settings")]
    public bool findEnemy = false;
    public bool findFriendly = true;
    public bool findPlayer = true;
    public bool findStructure = false;

    public float trackRange = 100f;

    
    

    void TrackTarget()
    {

        Collider[] targets = Physics.OverlapSphere(transform.position,trackRange);

        float nearestDist = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider target in targets)
        {
            if (target.CompareTag("Untagged") || target.CompareTag("Projectile")) continue;    // Ignore everything that's Untagged
            if (target.CompareTag("Enemy") && findEnemy != true) continue;
            if (target.CompareTag("Friendly") && findFriendly != true) continue;
            if (target.CompareTag("Player") && findPlayer != true) continue;
            if (target.CompareTag("Structure") && findStructure != true) continue;



            //    Vector3 pointAB = target.transform.position - transform.position;
            getDistToTarget = Vector3.Distance(transform.position,target.transform.position);
            
            Debug.DrawLine(target.transform.position, transform.position, Color.white);
            if (getDistToTarget <= nearestDist)
            {
                nearestDist = getDistToTarget;
                closestTarget = target.GetComponent<Transform>();
                
            }

            if (closestTarget != null && nearestDist < trackRange)
            {
                myTarget = closestTarget.transform;
                
                Debug.Log("TrackTestHasTarget");
            }
            else
            {
                myTarget = null;
                Debug.Log("TrackTestNull");
            }
            Debug.Log("nearest target " + nearestDist + " " + closestTarget + " " + myTarget);

            




         //   myTarget = closestTarget.GetComponent<Transform>();   // Finalized Target

 

        }

        

    }

    IEnumerator checkForTargets()
    {
        while (true)
        {
            TrackTarget();
            yield return new WaitForSeconds(0.3f);
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
        Gizmos.DrawWireSphere(transform.position,trackRange);

    }

}
