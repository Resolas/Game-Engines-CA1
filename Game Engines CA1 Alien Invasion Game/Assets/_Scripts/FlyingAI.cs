using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : MonoBehaviour
{


    private Rigidbody myRB;
    public Transform myTarget;

    [Header("AI Settings")]

    public FiringSys[] myWeapons;
    public bool findEnemy;
    public bool findFriendly;
    public bool findPlayer;
    public bool findStructure;

    public float trackRange = 500; // range to start following target
    public float range = 250;       // range to start attacking target

    [Header("Movement Settings")]
    public float speed = 10f;
    public float turnSpeed = 10f;
    public float defaultAltitude = 250f;
    public float altTolerance = 2;
    
    public float optimalRange  = 100;
    public float optimalTolerance = 2;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        TrackTarget();
        AIDecision();
        FireWeaponsSystems();
    }

    void AIDecision()
    {
        
        if (myTarget != null)
        {
            myRB.drag = 0.1f;
            transform.LookAt(new Vector3(myTarget.position.x, transform.position.y, myTarget.position.z));

            if (transform.position.y < myTarget.position.y - altTolerance)  // Altitude Control
            {
                myRB.AddForce(Vector3.up * speed * Time.deltaTime);
            }
            else if (transform.position.y > myTarget.position.y + altTolerance)
            {
                myRB.AddForce(Vector3.down * speed * Time.deltaTime);
            }

            if (inAttackRange < optimalRange - optimalTolerance)
            {
                myRB.AddRelativeForce(Vector3.back * speed * Time.deltaTime);
            }
            else if (inAttackRange > optimalTolerance + optimalTolerance)
            {
                myRB.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);

            }
        }
        else
        {
 
            myRB.velocity = new Vector3(0, 0, 0);
            myRB.drag = 5;

        }

    }

    void FireWeaponsSystems()
    {
        Debug.Log(inAttackRange);
        if (inAttackRange <= range)
        {

            for (int i = 0; i < myWeapons.Length; i++)
            {
                myWeapons[i].FireWeapons();

            }

        }

    }



    private float getDistToTarget;      // Gets the distance of all targets in range
    private float inAttackRange;        // Gets the distance of the nearest target
    void TrackTarget()
        {

            Collider[] targets = Physics.OverlapSphere(transform.position, trackRange);

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
                getDistToTarget = Vector3.Distance(transform.position, target.transform.position);   // Get dist between point A and B

                Debug.DrawLine(target.transform.position, transform.position, Color.white);
                if (getDistToTarget <= nearestDist)                                                 // If new object is closer than previous, becomes the new closest target
                {
                    nearestDist = getDistToTarget;
                    closestTarget = target.GetComponent<Transform>();

                }

                if (closestTarget != null && nearestDist < trackRange)      // If not null finalize the target
                {
                    myTarget = closestTarget.transform;
                inAttackRange = nearestDist;

                    Debug.Log("TrackTestHasTarget");
                }
                else
                {
                    myTarget = null;                // NOTE this does not seem to work, target is reset at IEnumerator
                    Debug.Log("TrackTestNull");
                }
             //   Debug.Log("nearest target " + nearestDist + " " + closestTarget + " " + myTarget);






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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, trackRange);
    }

}
