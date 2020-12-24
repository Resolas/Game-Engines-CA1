using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    private Text myText;

    public UniHPSys getHealth;
    string valueHP;

    // Update is called once per frame
    void Update()
    {

        myText.text = getHealth.health.ToString();

    }
}
