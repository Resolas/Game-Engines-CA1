﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getTarget = GetComponent<Turret>(); ;
        

    }

    [Header("Weapon Settings")]

    public GameObject[] myFirepoints;
    public GameObject myProjectile;
    public float maxCooldown = 1f;
    private float cooldown = 0;

    private Turret getTarget;    // Transfers the Target from Turret AIs to the Projectile in case it is guided

    // Update is called once per frame
    void FixedUpdate()
    {
        Cooling();
    }

    public void FireWeapons()       // To be called on other scripts
    {
        if (cooldown <= 0)
        {
            Transform setTarget = getTarget.myTarget;       // Sends the target data to this value
            for (int i = 0; i < myFirepoints.Length; i++)
            {

              GameObject projectile =  Instantiate(myProjectile, myFirepoints[i].transform.position, myFirepoints[i].transform.rotation);
                projectile.GetComponent<Projectile>().myHomingTarget = setTarget;       // The projectile gets the target data and uses it if it can seek
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
