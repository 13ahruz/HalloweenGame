using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelStarter : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;
    public bool bossLevelStarted = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && other.transform.GetComponent<PlayerController>().pumpkinCount < 50)
        {
            return;
        }
        else if (other.transform.CompareTag("Player") && other.transform.GetComponent<PlayerController>().pumpkinCount > 50)
        {
            GetComponent<BoxCollider>().enabled = false;
            bossLevelStarted = true;

        }
    }*/
}
