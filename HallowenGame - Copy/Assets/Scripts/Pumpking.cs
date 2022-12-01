using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Pumpking : MonoBehaviour

{
    public static bool destroyed = false;
    int destroyCount = 1;
    [SerializeField]
    Color pumpkinColor = Color.blue;
    [SerializeField]
    Material pumpkinMaterial;

    public bool isTouched = false;





    void Start()
    {
        isTouched = false;
    }
    /*void Update()
    {
        StartCoroutine(destroyTime());

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            destroyed = true;
        }
    }

    IEnumerator destroyTime()
    {
        if (destroyed)
        {
            var seq = DOTween.Sequence();
            transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }*/

}
