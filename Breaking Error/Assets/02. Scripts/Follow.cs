using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // 따라갈 target;
    public Vector3 offset; // 카메라 보정값

    void Start()
    {
        
    }

    void Update()
    {
        // 매 프레임 카메라 움직임
        transform.position = target.position + offset;
    }
}
