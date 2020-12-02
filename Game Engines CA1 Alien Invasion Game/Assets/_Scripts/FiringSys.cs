using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    [Header("Weapon Settings")]

    public GameObject[] myFirepoints;
    public GameObject myProjectile;
    public float maxCooldown = 1f;
    private float cooldown = 0;


    // Update is called once per frame
    void FixedUpdate()
    {
        Cooling();
    }

    public void FireWeapons()       // To be called on other scripts
    {
        if (cooldown <= 0)
        {
            for (int i = 0; i < myFirepoints.Length; i++)
            {

                Instantiate(myProjectile, myFirepoints[i].transform.position, myFirepoints[i].transform.rotation);

            }
            cooldown = maxCooldown;
        }

    }

    void Cooling()
    {

        if (cooldown > 0)
        {
            cooldown -= 1 * Time.deltaTime;
        }

    }


}
