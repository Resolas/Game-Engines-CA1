using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("Settings")]
    public int damage = 1;
    public int explodeDamage = 1;
    public float explodeRadius = 5f;
    public float speed = 10f;
    public float lifetime = 5f;
    public bool hitMultiple = false;
    public bool isExplosive = false;
    public bool isMelee = false;
    public bool isHoming = false;

    [Header("Effect Settings")]
    public GameObject[] effectsOnHit;

    // Update is called once per frame
    void FixedUpdate()
    {


        lifetime -= 1 * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {

            UniHPSys otherHealth = collision.collider.GetComponent<UniHPSys>();

        }

        for (int i = 0; i < effectsOnHit.Length; i++)
        {
            Instantiate(effectsOnHit[i],transform.position,Quaternion.identity);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explodeRadius);
    }

}
