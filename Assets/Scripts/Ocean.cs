using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    [SerializeField] private float scale = 2f;
    [SerializeField] private Material _oceanMat;
    public GameObject plane;
    
    private void Awake()
    {
        plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = Vector3.one * scale;
        plane.GetComponent<Renderer>().material = _oceanMat;
        plane.GetComponent<MeshCollider>().enabled = false;
    }
}
