using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Camera startCam;
    public Camera playerCam;

    public GameObject getPlayer;
    public GameObject getTextStart;
    public GameObject getTextPlayer;
    private bool started = false;


    // Update is called once per frame
    void Update()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && started != true)
        {
            startCam.enabled = false;

            getTextStart.SetActive(false);
            getTextPlayer.SetActive(true);
            getPlayer.SetActive(true);


            playerCam.enabled = true;   // will already be turned on by activating player



            started = true;


        }



    }

}
