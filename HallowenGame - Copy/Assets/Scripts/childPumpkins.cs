using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class childPumpkins : MonoBehaviour
{
    bool jumped;
    Vector3 randomLocation;
    float dropRate = 1;
    float dropSpeed = 1;
    float maxDropSpeed;
    void Start()
    {
        if (transform.childCount != 0)
        {
            transform.position = transform.GetChild(0).transform.localPosition;
        }
        jumped = false;

    }

    void Update()
    {
        /*if (transform.parent != null)
        {


            if ((GetComponentInParent<Pumpking>().isTouched == true) && !jumped)
            {
                transform.SetParent(null);
                Vector3 randomLocation = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
                //transform.DOLocalJump(transform.position + randomLocation, 2, 1, 0.5f);
                jumped = true;
            }
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {

        {
            if (other.transform.CompareTag("Player"))
            {
                Vector3 randomLocation = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));
                transform.DOLocalJump(transform.position + randomLocation, 2, 1, 0.5f)
                .Join(transform.DOScale(new Vector3(123.6f, 123.6f, 96.83194f), 0.2f))
                .Join(transform.DOScale(new Vector3(60, 60, 47.0058f), 0.3f));
                gameObject.layer = 7;
                gameObject.tag = "Pumpkin";

            }
        }

    }
}
