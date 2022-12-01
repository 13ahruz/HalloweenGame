using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject ourPlayer;
    [SerializeField]
    LayerMask playerLayer;
    bool followPlayerBool = false;
    NavMeshAgent nMesh;
    [SerializeField]
    Vector3 randomDestination;
    [SerializeField]
    bool touched = false;
    Vector3 startDestination;
    [SerializeField]
    Vector3 newDist;
    [SerializeField]
    bool arrived = true;
    [SerializeField]
    bool startWalk = true;
    bool isFinished = true;
    Animator anim;
    Rigidbody rb;








    void Start()
    {
        nMesh = GetComponent<NavMeshAgent>();
        startDestination = transform.position;
        StartCoroutine(walkAround());
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        followPlayer();
    }

    public Vector3 RandomVector()
    {
        Vector3 newDestination = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        return newDestination;
    }



    void followPlayer()
    {
        Debug.DrawRay(transform.position, (ourPlayer.transform.position - transform.position), Color.red, 6f);
        if (Physics.Raycast(transform.position, (ourPlayer.transform.position - transform.position), 6f, playerLayer))
        {
            Invoke("Destroy", 10f);
            anim.SetBool("Run", true);
            StopCoroutine(walkAround());
            nMesh.destination = ourPlayer.transform.position;
        }

    }

    IEnumerator walkAround()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            if (startWalk)
            {
                newDist = startDestination + RandomVector();
                nMesh.SetDestination(newDist);
                startWalk = false;
                anim.SetTrigger("Walk");
            }

            if ((transform.position.x == newDist.x) && (transform.position.z == newDist.z))
            {
                startWalk = true;
            }
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ourPlayer.transform.GetComponent<PlayerController>().health -= 5;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BulletPumpkin"))
        {
            Destroy(gameObject);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}


