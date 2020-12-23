using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniHPSys))]
public class BuildingCollapse : MonoBehaviour
{
    // Destroys objects that are not connected to any building below via raycast

    // Start is called before the first frame update
    void Start()
    {
        myHPSys = GetComponent<UniHPSys>();
     //   StartCoroutine(CheckBottom());
    }

    private UniHPSys myHPSys;
    

    private float rayDist = 10f;
    IEnumerator CheckBottom()
    {

        while (true)
        {
            Vector3 offset = new Vector3(0,-10,0);
            RaycastHit hit;


            if (Physics.Raycast(transform.position + offset, Vector3.down, out hit ,rayDist) != true)
            {
                if (hit.collider.CompareTag("Structure") != true)
                {
                    Debug.Log("DESTROY ABOVE");
                    myHPSys.isDead = true;
                }
                
            }
            Debug.Log("RUN CO TEST");
            Debug.DrawLine(transform.position + offset, transform.position + offset + new Vector3(0,-rayDist,0),Color.green);
            yield return new WaitForSeconds(1f);
        }

    }

}
