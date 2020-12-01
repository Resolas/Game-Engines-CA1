using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    //    myPos = GetComponent<Vector3>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    Rigidbody myRB;
  //  Vector3 myPos;
    public GameObject rotYCam;

    private float horz, vert;
    private float height;
    public float speed = 500f;
    public float rotSpeed = 50f;
    public float jumpPower = 10f;

    public bool isFlying;
    public bool onGround;
    private bool mouseLock = true;

    public FiringSys[] myWeaponSystems = new FiringSys[3];

    // Update is called once per frame
    void Update()
    {

        GroundChecker();
        Movement();
        MouseLock();
    }

    void Movement()
    {

        horz = Input.GetAxis("Horizontal"); // Strafe side to side
        vert = Input.GetAxis("Vertical");   // Move forwards & backwards

        if (isFlying)   // Flight Mode
        {
            

            myRB.useGravity = false;
            height = Input.GetAxis("Fly");   // Ascend & Descend
            myRB.AddForce(Vector3.up * speed * height * Time.deltaTime);




        }
        else {
            myRB.useGravity = true;
        }


        myRB.AddRelativeForce(Vector3.right * speed * horz * Time.deltaTime);
        myRB.AddRelativeForce(Vector3.forward * speed * vert * Time.deltaTime);

        

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            myRB.velocity = new Vector3(myRB.velocity.x,jumpPower,myRB.velocity.z);

            
        }
        else if (onGround != true && Input.GetKeyDown(KeyCode.Space)) isFlying = true;       // sets it to flying while not on ground

        transform.Rotate(0,Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime,0);    // Rotate the player with mouse
        rotYCam.transform.Rotate(-Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime,0,0); // rotate child object to look up and down
    }

    void MouseLock()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }


    }

    void GroundChecker()
    {

        if (Physics.Raycast(transform.position, Vector3.down,1.1f))
        {
            onGround = true;
            isFlying = false;
        }
        else
        {
            onGround = false;
        }
    }

    void FireWeaponry()
    {

        if (Input.GetMouseButton(0)) myWeaponSystems[0].FireWeapons();


        if (Input.GetMouseButton(1)) myWeaponSystems[1].FireWeapons();


        if (Input.GetKeyDown(KeyCode.Q)) myWeaponSystems[2].FireWeapons();


    }

}
