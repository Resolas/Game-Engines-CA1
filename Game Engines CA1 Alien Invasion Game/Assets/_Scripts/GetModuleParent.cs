using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetModuleParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myShipCoreHealth = GetComponentInParent<UniHPSys>();
    }
    [SerializeField]
    UniHPSys myShipCoreHealth;

    public int moduleHealth = 50;   // Modules have a seperate health system determining the individual part health instead.
    public GameObject deathEffect;

    // Update is called once per frame
    void Update()
    {
        if (moduleHealth <= 0 || myShipCoreHealth.health <= 0)
        {
            Destroy(gameObject);
            if (deathEffect != null) Instantiate(deathEffect,transform.position,Quaternion.identity);

        }

        

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            // Both module and overall ship health takes damage
            moduleHealth -= collision.collider.GetComponent<Projectile>().damage;
            myShipCoreHealth.health -= collision.collider.GetComponent<Projectile>().damage/2;  // main health takes half damage instead

        }
    }

    

}
