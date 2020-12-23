using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getTarget = GetComponent<Turret>();
        

    }

    [Header("Weapon Settings")]

    public GameObject[] myFirepoints;
    public GameObject myProjectile;
    public float maxCooldown = 1f;
    private float cooldown = 0;
    public float accuracy = 0.5f;
    public bool staggerFire = false;
    private int stagFireWep = 0;
    public bool getTargetData = true;

    private Turret getTarget;    // Transfers the Target from Turret AIs to the Projectile in case it is guided
    public Transform setTarget;       // Sends the target data to this value

    // Update is called once per frame
    void FixedUpdate()
    {
        if (getTarget != null) setTarget = getTarget.myTarget;
        Cooling();
    }

    public void FireWeapons()       // To be called on other scripts
    {
        Debug.Log("FIRE + " + gameObject);
        if (cooldown <= 0)
        {
            
            
            if (staggerFire != true)
            {
                for (int i = 0; i < myFirepoints.Length; i++)
                {

                    GameObject projectile = Instantiate(myProjectile, myFirepoints[i].transform.position, myFirepoints[i].transform.rotation * Quaternion.Euler(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy)));
                    if (setTarget != null) projectile.GetComponent<Projectile>().myHomingTarget = setTarget;       // The projectile gets the target data and uses it if it can seek

                }
            }
            else
            {
                int max = myFirepoints.Length;
                GameObject projectile = Instantiate(myProjectile, myFirepoints[stagFireWep].transform.position, myFirepoints[stagFireWep].transform.rotation * Quaternion.Euler(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy)));
                if (setTarget != null) projectile.GetComponent<Projectile>().myHomingTarget = setTarget;       // The projectile gets the target data and uses it if it can seek


                stagFireWep++;
                if (stagFireWep >= max) stagFireWep = 0;
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
