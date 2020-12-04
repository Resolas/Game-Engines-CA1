using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isRandom)
        {
            rotSpeedx = Random.Range(-randSpeed,randSpeed);
            rotSpeedy = Random.Range(-randSpeed, randSpeed);
            rotSpeedz = Random.Range(-randSpeed, randSpeed);
        }
        

    }
    [Header("Settings")]
    public float rotSpeedx = 10;
    public float rotSpeedy = 10;
    public float rotSpeedz = 10;

    public bool isRandom = false;
    public float randSpeed = 10;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotSpeedx * Time.deltaTime,rotSpeedy * Time.deltaTime,rotSpeedz * Time.deltaTime);
    }
}
