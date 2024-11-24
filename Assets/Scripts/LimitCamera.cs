using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LimitCamera : MonoBehaviour
{
    public CinemachineConfiner virtualCamera;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineConfiner>();
        virtualCamera.m_BoundingShape2D = GameObject.Find("Barreras").GetComponent<PolygonCollider2D>();
    }
}
