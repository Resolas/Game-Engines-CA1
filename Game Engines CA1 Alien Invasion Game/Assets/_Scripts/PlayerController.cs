using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myPos = GetComponent<Vector3>();
    }

    Rigidbody myRB;
    Vector3 myPos;

    private float horz, vert;
    private float height;
    public float speed = 5f;

    private bool isFlying;
    private bool onGround;


    // Update is called once per frame
    void Update()
    {


        Movement();

    }

    void Movement()
    {

        horz = Input.GetAxis("Horizontal"); // Strafe side to side
        vert = Input.GetAxis("Vertical");   // Move forwards & backwards

        if (isFlying)
            height = Input.GetAxis("Height");   // Ascend & Descend

        myRB.AddRelativeForce(Vector3.right * speed * horz);
        myRB.AddRelativeForce(Vector3.forward * speed * vert);

        

        transform.Rotate(0,Input.GetAxis("Mouse X") * speed * Time.deltaTime,0);    // Rotate the player with mouse
    }

}
