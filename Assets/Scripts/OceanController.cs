using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanController : MonoBehaviour
{
    [SerializeField] private float _maxHeight = 10f;
    [SerializeField] private float _maxWaveHeight = 0.7f;
    [SerializeField] private Material _oceanMat;
    [SerializeField] private float _step = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.up * (Time.deltaTime * _step);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position -= Vector3.up * (Time.deltaTime * _step);
        }
    }
}
