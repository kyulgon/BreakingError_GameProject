using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Melee, // 근접 공격
        Range // 원거리 공격
    }
    public Type type;
    public int damage;
    public float rate; // 공격 속도
    public BoxCollider meleeArea; // 사정거리
    public TrailRenderer trailEffect; // 꼬리모양 이펙트
        
    public Transform bulletPos; // 총알 위치
    public Transform bulletCasePos; // 탄피 나갈 위치
    public GameObject bullet; // 총알 프리팹
    public GameObject bulletCase; // 탄피 프리팹

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if (type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // #1. 발사
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 100;

        yield return null;
        // #2. 탄피 배출
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid  = intantCase.GetComponent<Rigidbody>();
        
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse); // 탄피 회전 (Impulse : 즉발)
    }
  

}
