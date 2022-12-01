using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BossController : MonoBehaviour
{
    [SerializeField]
    List<string> bossAnims;
    Animator anim;
    int randomA;
    [SerializeField]
    ParticleSystem particle;
    public bool particleOn;

    [SerializeField]
    Behaviour attacks;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform particle3Parent;
    [SerializeField]
    Transform particlePos;
    [SerializeField]
    GameObject particle2Parent;
    [SerializeField]
    Transform attack2Pos;

    [SerializeField]
    List<GameObject> attack2Particles;
    [SerializeField]
    List<ParticleSystem> attack1Particles;
    public int bossHealth = 100;
    bool test1;
    [SerializeField]
    TextMeshProUGUI text1;


    private void Awake()
    {
        Attack1Stop();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(bossAttackAnims());
        particle.Stop();
        bossHealth = 100;

    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().bossLevelStarted)
        {
            randomA = Random.Range(0, 2);
            transform.LookAt(player.transform.position);

            particlePos.position = Vector3.Lerp(particlePos.position, player.transform.position, Time.time * 0.0005f);
            particle3Parent.LookAt(particlePos);
            Attack2ParticleMovement();
        }


        if (bossHealth <= 0)
        {
            if (test1 == true)
            {
                StartCoroutine(bossLevel());
                test1 = false;

            }
            particle.Stop();
            foreach (var part in attack1Particles)
            {
                part.Stop();
            }
            foreach (var part in attack1Particles)
            {
                part.Stop();
            }
            anim.SetTrigger("Death");
        }

    }

    IEnumerator bossAttackAnims()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            anim.SetTrigger(bossAnims[randomA]);
        }
    }

    void Attack3Particle()
    {
        particle.Play();
    }

    void Attack3ParticleStop()
    {
        particle.Stop();
    }

    void Attack2()
    {
        attack2Particles.Clear();
        for (int i = 0; i <= 4; i++)
        {
            var seq = DOTween.Sequence();
            GameObject particle2Par = Instantiate(particle2Parent, attack2Pos.position, Quaternion.Euler(0, (180 - (i * 45)), 0));
            attack2Particles.Add(particle2Par);
        }
    }

    void Attack2ParticleMovement()
    {
        if ((attack2Particles.Count >= 5) && (attack2Particles[4] != null))
        {
            attack2Particles[0].transform.position += new Vector3(0.1f, 0, 1) * 3f * Time.deltaTime;
            attack2Particles[1].transform.position += new Vector3(1, 0, 1) * 3f * Time.deltaTime;
            attack2Particles[2].transform.position += new Vector3(1, 0, 0) * 3f * Time.deltaTime;
            attack2Particles[3].transform.position += new Vector3(1, 0, -1) * 3f * Time.deltaTime;
            attack2Particles[4].transform.position += new Vector3(0.1f, 0, -1) * 3f * Time.deltaTime;
        }
    }

    void Attack1Play()
    {
        for (int i = 0; i <= 4; i++)
        {
            attack1Particles[i].Play();
        }
    }

    void Attack1Stop()
    {
        for (int i = 0; i <= 4; i++)
        {
            attack1Particles[i].Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BulletPumpkin"))
        {
            bossHealth -= 4;
        }
    }

    IEnumerator bossLevel()
    {
        text1.text = "BOSS LEVEL SUCCEED";
        yield return new WaitForSeconds(1.4f);
        Destroy(text1);
    }
}
