using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletPumpkin : MonoBehaviour
{

    void Start()
    {


    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }


}
