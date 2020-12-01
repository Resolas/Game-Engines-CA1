using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    Rigidbody myRB;

    [Header("Settings")]
    public float speed = 5000;
    public float altSpeed = 5000;
    public float angSpeed = 10;
    public bool isDumbAI = true;
    public float maintainAlt;


    private LayerMask myRayAltitudeLayer = 8;
    private bool rayBool;

    // Update is called once per frame
    void Update()
    {

        AIMovement();

    }

    void AIMovement()
    {

        if (isDumbAI == true)   // Moves straight forward thats it
        {
            myRB.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
        }

        float xPos = transform.position.x, yPos = transform.position.y, zPos = transform.position.z;
        Vector3 rayPos = new Vector3(xPos,yPos,zPos);
        
        RaycastHit hit = new RaycastHit();
        myRayAltitudeLayer = ~myRayAltitudeLayer;   // Inverts so it will ONLY collide with layer 8

        if (Physics.Raycast(rayPos, Vector3.down, maintainAlt, myRayAltitudeLayer))
        {
            rayBool = true;
            Debug.Log("AltHIT");
        }
        else
        {
            rayBool = false;
            Debug.Log("AltNOHIT");
        }

        if (rayBool)
        {
            myRB.AddRelativeForce(Vector3.up * altSpeed * 2 * Time.deltaTime);

        }
        else if (rayBool != true)
        {
            myRB.AddRelativeForce(Vector3.down * altSpeed * Time.deltaTime);
        }

    }
}
