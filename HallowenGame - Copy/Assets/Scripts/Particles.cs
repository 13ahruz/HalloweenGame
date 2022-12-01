using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    bool increased = false;
    [SerializeField]
    float attackSpeed = 1;
    [SerializeField]
    float attackRate = 1;
    [SerializeField]
    float nextAttackTime;
    void Start()
    {
        //StartCoroutine(increasedStoper());

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnParticleCollision(GameObject other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("One");
            if (Time.time >= nextAttackTime)
            {
                Debug.Log("Hit");
                PlayerController.instance.health -= 10;
                increased = true;
                nextAttackTime = Time.time + attackSpeed / attackRate;
            }


        }
    }
    /* IEnumerator increasedStoper()
     {
         while (true)
         {
             if (increased)
             {
                 yield return new WaitForSeconds(1f);
                 increased = false;
             }
         }

     }*/
}
