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
    public GameObject[] myGunArms;
    public float range = 250f;
    private float closest;
    public bool isFriendly = false;
    public float getDistToTarget;
    public bool isRot360 = true;

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.DrawLine(myTarget.transform.position, transform.position, Color.red);

        

        if (myTarget != null)
        {
            if (isRot360 != true && myGunArms != null)
            {
                Quaternion look = Quaternion.LookRotation(myTarget.position, transform.position);
                transform.LookAt(new Vector3(myTarget.position.x, transform.position.y, myTarget.position.z));
                for (int i = 0; i < myGunArms.Length; i++)
                {
                //    myGunArms[i].transform.rotation = Quaternion.Euler(look.eulerAngles.x,0,0);
                //    myGunArms[i].transform.LookAt(new Vector3(myTarget.transform.position.x, myGunArms[i].transform.position.y,myGunArms[i].transform.position.z));
                    
                    myGunArms[i].transform.LookAt(myTarget);
                }
            }
            else
            {
                transform.LookAt(myTarget);
            }
            



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

//    public float trackRange = 100f;

    
    

    void TrackTarget()
    {

        Collider[] targets = Physics.OverlapSphere(transform.position,range);

        float nearestDist = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider target in targets)
        {
            if (target.CompareTag("Untagged") || target.CompareTag("Projectile") || target.CompareTag("Ground")) continue;    // Ignore everything that's Untagged
            if (target.CompareTag("Enemy") && findEnemy != true) continue;
            if (target.CompareTag("Friendly") && findFriendly != true) continue;
            if (target.CompareTag("Player") && findPlayer != true) continue;
            if (target.CompareTag("Structure") && findStructure != true) continue;



            //    Vector3 pointAB = target.transform.position - transform.position;
            getDistToTarget = Vector3.Distance(transform.position,target.transform.position);   // Get dist between point A and B
            
            Debug.DrawLine(target.transform.position, transform.position, Color.white);
            if (getDistToTarget <= nearestDist)                                                 // If new object is closer than previous, becomes the new closest target
            {
                nearestDist = getDistToTarget;
                closestTarget = target.GetComponent<Transform>();
                
            }

            if (closestTarget != null && nearestDist < range)      // If not null finalize the target
            {
                myTarget = closestTarget.transform;
                
                Debug.Log("TrackTestHasTarget");
            }
            else
            {
                myTarget = null;                // NOTE this does not seem to work, target is reset at IEnumerator
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
            myTarget = null;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);

    }

}
