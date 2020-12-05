using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();

        if (isTrigger)
        {
            myCol.isTrigger = true;
        }
        else
        {
            myCol.isTrigger = false;
        }
    
        if (isConSpeed != true) // Impulse for instant speed and drag to zero for bullets
        {
            myRB.drag = 0;
            myRB.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

        }
        
    }

    [Header("Settings")]
    public int damage = 1;
    public int explodeDamage = 1;
    public float explodeRadius = 5f;
    public float speed = 10f;
    public float rotSpeed = 100f;
    public bool isConSpeed = false;
    public float lifetime = 5f;
    public bool hitMultiple = false;
    public bool isExplosive = false;
    public bool isMelee = false;
    public bool isHoming = false;

    public bool isTrigger = false;

    private Rigidbody myRB;
    private Collider myCol;
  //  [HideInInspector]
    public Transform myHomingTarget;

    [Header("Effect Settings")]
    public GameObject[] effectsOnHit;
    public GameObject[] leaveExEffectsOnHit;    // unparents existing effect on hit

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isHoming)
        {
            ModeHoming();
        }
        else if (isConSpeed == true)
        {
            myRB.velocity = transform.forward * speed;
        }

        Lifetime();
       
    }

    void Lifetime()
    {
        lifetime -= 1 * Time.deltaTime;
        if (lifetime < 0) Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {

            UniHPSys otherHealth = collision.collider.GetComponent<UniHPSys>();
            otherHealth.health -= damage;

        }

        for (int i = 0; i < effectsOnHit.Length; i++)
        {
            Instantiate(effectsOnHit[i],transform.position,Quaternion.identity);
        }

        for (int i = 0; i < leaveExEffectsOnHit.Length; i++)
        {
            leaveExEffectsOnHit[i].transform.parent = null;
        }

        if (isExplosive) ModeExplosive();


        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Projectile") && other.GetComponent<Collider>().isTrigger) return;     // Ignores collision of both triggers due to RBs

        if (other.CompareTag("Player"))
        {

            UniHPSys otherHealth = other.GetComponent<UniHPSys>();
            otherHealth.health -= damage;

        }

        for (int i = 0; i < effectsOnHit.Length; i++)
        {
            Instantiate(effectsOnHit[i], transform.position, Quaternion.identity);
        }

        for (int i = 0; i < leaveExEffectsOnHit.Length; i++)
        {
            leaveExEffectsOnHit[i].transform.parent = null;
        }

        if (isExplosive) ModeExplosive();

        

        Destroy(gameObject);
    }


    void ModeMelee()
    {



        Destroy(gameObject);

    }

    void ModeExplosive()
    {

        Collider[] expHit = Physics.OverlapSphere(transform.position,explodeRadius);

        foreach (Collider hit in expHit)
        {

            if (hit.CompareTag("Enemy") || hit.CompareTag("Friendly") || hit.CompareTag("Player") || hit.CompareTag("Structure"))
            {

                if (hit.GetComponent<UniHPSys>() != null)
                {
                    hit.GetComponent<UniHPSys>().health -= explodeDamage;
                    Debug.Log(hit + " Damaged");
                }

                if (hit.GetComponent<GetModuleParent>() != null)
                {
                    hit.GetComponent<GetModuleParent>().moduleHealth -= explodeDamage;
                    Debug.Log(hit + " Module Damaged");
                }

            }

        }


    }

    void ModeHoming()
    {

        myRB.velocity = transform.forward * speed;

        var targetRot = Quaternion.LookRotation(myHomingTarget.position - transform.position);

        myRB.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime));
    //    myRB.MoveRotation(Quaternion.LookRotation(Vector3.forward, Vector3.up));

        #region 2D homing?
        /*
        Vector3 direction = myHomingTarget.position - myRB.position;

        direction.Normalize();

        float rotateAmountx = Vector3.Cross(direction, transform.forward).x;
        float rotateAmounty = Vector3.Cross(direction, transform.forward).y;

        myRB.angularVelocity = rotateAmountx * rotSpeed;

        myRB.velocity = transform.forward * speed;
        */
        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explodeRadius);
    }

}
