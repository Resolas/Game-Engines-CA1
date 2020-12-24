using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniHPSys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (instantiateOnPos == null)   // if null default to its pivot position
        {
            instantiateOnPos = GetComponent<Transform>();
        }


    }

    private void OnEnable()
    {
        if (isPlayer) getCam = Camera.main.gameObject;

    }

    public int health = 100;
    public bool isUnit;
    public bool isPlayer;
    public bool isStructure;
    public bool isDead;
    public bool destroyImmOnDeath;

    [Header("Effect Settings")]
    public bool playDeathEffectNow;
    public GameObject deathEffect;
    public GameObject subExplosions;
    public float subExpRange = 10;

    public Transform instantiateOnPos;
    private bool playOnce = false;
    private GameObject getCam;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0) isDead = true;

        if (isDead)
        {
            if (isPlayer) getCam.transform.parent = null;

            if (playDeathEffectNow) Instantiate(deathEffect,instantiateOnPos.position,Quaternion.identity);

            if (isDead && destroyImmOnDeath != true)
            {
                StartCoroutine(PlaySubExplosions());
                StartCoroutine(PlayDeathEffect());
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        

    }


    IEnumerator PlayDeathEffect()
    {

        while (true)
        {
            if (playDeathEffectNow != true && playOnce == false)
            {
                Instantiate(deathEffect, instantiateOnPos.position, Quaternion.identity);
                playOnce = true;
            }
                

            yield return new WaitForSeconds(2f);

            Destroy(gameObject);

            
        }

    }

    IEnumerator PlaySubExplosions()
    {
        while (true)
        {
            float randX = Random.Range(-subExpRange, subExpRange);
            float randY = Random.Range(-subExpRange, subExpRange);
            float randZ = Random.Range(-subExpRange, subExpRange);

            Instantiate(subExplosions, instantiateOnPos.position + new Vector3(randX,randY,randZ), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0,2));
        }

    }
}
