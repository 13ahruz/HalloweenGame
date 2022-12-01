using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(14f);
        Destroy(gameObject);
    }
}
