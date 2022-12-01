using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    FloatingJoystick joystick;
    [SerializeField]
    float movementSpeed;
    float horizontal;
    float vertical;
    float currentVelocity;
    float smoothTurnTime = 0.1f;
    Vector3 direction;
    Rigidbody rb;
    bool isGrounded;
    [SerializeField]
    float jumpForce;
    Collider[] pumpkins;
    Collider[] childPumpkins;
    [SerializeField]
    Transform holdPos;
    [SerializeField]
    Transform dedectPos;
    public int pumpkinCount;
    [SerializeField]
    LayerMask pumpkinLayer;
    public bool bossLevelStarted = false;
    [SerializeField]
    SplineFollower _spline;
    [SerializeField]
    Transform bossPos;
    public static PlayerController instance;
    [SerializeField]
    public int health = 100;
    [SerializeField]
    LayerMask collectablePumpkin;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float attackRate = 1;
    float attackSpeed = 0.5f;
    float nextAttackTime = 0;
    [SerializeField]
    Transform playerParent;
    [SerializeField]
    Transform bossLevelStarter;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject insideCamera;
    [SerializeField]
    GameObject bossCamera;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform shootPos;
    [SerializeField]
    GameObject chanta;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Transform bossHead;
    [SerializeField]
    TextMeshProUGUI text1;
    bool test = true;





    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody>();
        _spline = GetComponentInParent<SplineFollower>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

    }
    void Update()
    {
        //Jump();
        Move();
        CollectPumpkins();
        DestroyPumpkins();
        DedectEnemies();
        text.text = health.ToString();

        if ((pumpkinCount > 35) && test == true)
        {
            StartCoroutine(bossLevel());
            test = false;

        }



        if (bossLevelStarted)
        {
            if ((Time.time > nextAttackTime) && (boss.transform.GetComponent<BossController>().bossHealth > 0))
            {
                GameObject spawnedBullet = Instantiate(bullet, shootPos.position, Quaternion.Euler(0, 0, 0));
                nextAttackTime = Time.time + 1f;
                var seq1 = DOTween.Sequence();
                spawnedBullet.transform.DOMove(bossHead.transform.position, 0.4f);

            }
            boss.SetActive(true);
            insideCamera.SetActive(false);
            _spline.enabled = true;
            var seq = DOTween.Sequence();
            seq.Append(transform.DOLocalMove(new Vector3(0, 0, 0), 1f))
            .AppendInterval(1f)
            .AppendCallback(() =>
            {
                transform.LookAt(bossPos.position);
                bossCamera.SetActive(true);
                //transform.DOLocalMove(new Vector3(-34, 5.87f, -19), 0f);
            });

            _spline.followSpeed = Mathf.Abs(horizontal) * movementSpeed;

            if (horizontal < 0)
            {
                _spline.direction = Spline.Direction.Backward;
            }
            else
            {
                _spline.direction = Spline.Direction.Forward;
            }
            transform.LookAt(bossPos.position);

            return;

        }

        /*if ((horizontal > 0) && bossLevelStarted)
        {
            anim.SetBool("WalkR", true);
            anim.SetBool("WalkL", false);
        }
        else if ((horizontal > 0) && bossLevelStarted)
        {
            anim.SetBool("WalkL", true);
            anim.SetBool("WalkR", false);
        }
        else if ((horizontal == 0) && bossLevelStarted)
        {
            anim.SetTrigger("IdleAnim");
            anim.SetBool("WalkL", false);
            anim.SetBool("WalkR", false);
        }
*/

    }


    /*void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
}*/

    void Move()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0, vertical);
        if ((direction.magnitude > 0.01))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            rb.MovePosition(transform.position + (direction * movementSpeed * Time.fixedDeltaTime));
        }
        anim.SetFloat("Run", direction.magnitude);
    }



    void CollectPumpkins()
    {

        childPumpkins = Physics.OverlapSphere(dedectPos.position, 0.9f, pumpkinLayer);
        foreach (var pumpkin in childPumpkins)
        {
            if (pumpkin.transform.CompareTag("Pumpkin"))
            {

                pumpkin.tag = "CollectedPumpkin";
                var seq = DOTween.Sequence();
                //seq.Append(pumpkin.transform.DOLocalJump(holdPos.transform.position, 2, 1, 0.5f))
                seq.Append(pumpkin.transform.DOScale(new Vector3(1, 1, 1), 0.2f))
                .Join(pumpkin.transform.DOScale(new Vector3(0, 0, 0), 0.3f))
                .AppendCallback(() =>
                {
                    Destroy(pumpkin);
                });
                pumpkinCount++;
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.transform.CompareTag("BossLevelStarter") && pumpkinCount < 35)
        {
            return;
        }
        else if (other.transform.CompareTag("BossLevelStarter") && pumpkinCount > 35)
        {
            bossLevelStarter.GetComponent<BoxCollider>().enabled = false;
            bossLevelStarted = true;
            anim.SetTrigger("Boss");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(dedectPos.position, 0.9f);
    }

    void DestroyPumpkins()
    {

        pumpkins = Physics.OverlapSphere(dedectPos.position, 0.9f, collectablePumpkin);
        foreach (var pumpkin in pumpkins)
        {
            if (pumpkin.transform.CompareTag("Collectable"))
            {

                var seq = DOTween.Sequence();
                seq.Append(pumpkin.transform.DOJump(chanta.transform.position, 2, 1, 0.5f))
                .Join(pumpkin.transform.DOScale(0, 0.5f))
               .AppendCallback(() =>
                {
                    Destroy(pumpkin);
                });

                pumpkinCount++;
            }
        }
    }

    void DedectEnemies()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            if (hit.transform.CompareTag("Enemy") && (Time.time > nextAttackTime))
            {
                GameObject spawnedBullet = Instantiate(bullet, shootPos.position, Quaternion.Euler(0, 0, 0));
                spawnedBullet.transform.LookAt(hit.transform.position);
                nextAttackTime = Time.time + 0.8f;
                spawnedBullet.transform.DOMove(hit.transform.position, 0.4f);

            }
        }
    }

    IEnumerator bossLevel()
    {
        text1.text = "BOSS LEVEL STARTED";
        yield return new WaitForSeconds(1.4f);
        Destroy(text1);
    }


}