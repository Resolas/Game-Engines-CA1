using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponTargeting : MonoBehaviour
{
    // Gets the position for player's weapons/firing systems to shoot and valid targeting data for homing weapons


    public Transform[] myWeaponsTrans = new Transform[3];
    public FiringSys[] myWeaponSys = new FiringSys[3];
    public bool[] getLockOn = new bool[3];
    public bool[] weaponLookPos = new bool[3];

    Camera myCamera;

    private void Start()
    {
        myCamera = Camera.main;
    }

    private void FixedUpdate()
    {

        GetFiringPosition();


    }


    void GetFiringPosition()
    {

        RaycastHit hit;

        if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, Mathf.Infinity))
        {

            var getPoint = hit.point;   // The position the ray hits
            Vector3 hitPos = getPoint;
            var getCol = hit.collider;  // The target data for homing weapons

            for (int i = 0; i < myWeaponsTrans.Length; i++)
            {
                if (weaponLookPos[i]) myWeaponsTrans[i].transform.LookAt(hitPos);

                if (getLockOn[i]) myWeaponSys[i].setTarget = getCol.transform;


            }

            Debug.Log("RAYPOS + " + hitPos + "  " + getCol.transform);
        }


    }

}
