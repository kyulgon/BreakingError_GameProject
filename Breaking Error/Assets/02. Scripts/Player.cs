using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapon;

    // 방향 변수
    float hAxis;
    float vAxis;


    bool wDown; // 걷기 (Shift)
    bool jDown; // 점프 (Space)
    bool iDown; // 아이템 줍기 (E)

    // 장비 바꾸기 변수
    bool sDown1;
    bool sDown2;

    // 점프 피하기 변수
    bool isJump;
    bool isDodge;

    bool isSwap; // 교체 시간차

    Vector3 moveVec; // 움직임 방향
    Vector3 dodgeVec; // 회피 도중 방향전환이 되지 않도록 회피방향

    Rigidbody rigid;
    Animator anim; // 애니메이터 관리

    GameObject nearObject; // 아이템 먹기
    GameObject equipWeapon; // 장착중인 무기
    int equipWeaponIndex = -1; // 장착중인 무기 인덱스


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Swap();
        Interation();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge) // 회피중이면 피해벡터를 넣어줌
            moveVec = dodgeVec;

        if (isSwap)
            moveVec = Vector3.zero;
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("IsRun", moveVec != Vector3.zero);
        anim.SetBool("IsWalk", wDown);        
    }

    void Turn()
    {
        // 나아가는 방향 바라보기
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if(jDown && moveVec == Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
            anim.SetBool("IsJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }        
    }
    void Dodge() // 물리 충돌 아님
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f); // 시간차
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;        
    }

    void Swap()
    {
        if (sDown1 && (!hasWeapon[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapon[1] || equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;
        if(sDown1) weaponIndex = 0;
        if(sDown2) weaponIndex = 1;

        if ((sDown1 || sDown2) && !isJump && !isDodge)
        {
            if(equipWeapon != null)
                equipWeapon.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);

            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f); // 시간차
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Interation()
    {
        if(iDown && nearObject != null && !isJump && !isDodge)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapon[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("IsJump", false);
            isJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;

        Debug.Log(nearObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }


}
