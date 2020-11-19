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

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {

            UniHPSys otherHealth = collision.collider.GetComponent<UniHPSys>();

        }

        Destroy(gameObject);
    }

    void ModeMelee()
    {



        Destroy(gameObject);

    }

    void ModeExplosive()
    {


    }

}
