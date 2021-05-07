using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public bool isChase;

    Rigidbody rigid;
    Material mat;
    NavMeshAgent nav;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();

        Invoke("ChaseStart", 2);
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void ChaseStart()
    {
        isChase = true;
    }

    private void Update()
    {
        if(isChase)
            nav.SetDestination(target.position);
    }

    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }       
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position; // 반작용 방향구하기
            StartCoroutine(OnDamage(reactVec));
        }
        else if(other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);

            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec) // 데미지 주는 코루틴
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 14; // 레이어 바꾸기
            isChase = false;
            nav.enabled = false;

            // 넉백
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec *5, ForceMode.Impulse);

            Destroy(gameObject, 3);
        }
    }
}
