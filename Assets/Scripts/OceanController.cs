using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanController : MonoBehaviour
{
    [SerializeField] private float _maxHeight = 10f;
    [SerializeField] private float _step = 1f;
    [SerializeField] private float _waveStep = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && transform.position.y < _maxHeight)
        {
            transform.position += Vector3.up * (Time.deltaTime * _step);
            float curChopines = GetComponent<Renderer>().material.GetFloat("_Chopines");
            GetComponent<Renderer>().material.SetFloat("_Chopines", curChopines + Time.deltaTime * _waveStep);
            Debug.Log(curChopines);
        }
        else if (transform.position.y > 0)
        {
            transform.position -= Vector3.up * (Time.deltaTime * _step);
            float curChopines = GetComponent<Renderer>().material.GetFloat("_Chopines");
            GetComponent<Renderer>().material.SetFloat("_Chopines", curChopines - Time.deltaTime * _waveStep);

        }
    }
}
