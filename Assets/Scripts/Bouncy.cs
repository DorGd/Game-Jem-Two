using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bouncy : MonoBehaviour
{

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _depthBeforeSubmerged = 1f;
    [SerializeField] private float _displacementAmount = 3f;
    [SerializeField] private int _floatersCount = 1;
    [SerializeField] private float _waterDrag = 0.99f;
    [SerializeField] private float _waterAngularDrag = 0.5f;
    private LayerMask oceanLayer;

    private void Awake()
    {
        oceanLayer = LayerMask.GetMask("Water");
    }

    private void FixedUpdate()
    {
        float distFromWave = getDistanceToWave(); 
        
        _rb.AddForceAtPosition(Physics.gravity / _floatersCount, transform.position, ForceMode.Acceleration);
        if (distFromWave < 0)
        {
            float displacementMultiplier = Mathf.Clamp01(-distFromWave / _depthBeforeSubmerged) * _displacementAmount;
            _rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),
                transform.position, ForceMode.Acceleration);
            _rb.AddForce(displacementMultiplier * -_rb.velocity * _waterDrag * Time.fixedTime, ForceMode.VelocityChange);
            _rb.AddTorque(displacementMultiplier * -_rb.angularVelocity * _waterAngularDrag * Time.fixedTime, ForceMode.VelocityChange);
        }
    }

    private float getDistanceToWave()
    {
        RaycastHit hit;
        float distanceToWave = 0;
        
        if (Physics.Raycast (transform.position, Vector3.down, out hit , Mathf.Infinity, oceanLayer))
        {
            distanceToWave = hit.distance;
        }
        else if (Physics.Raycast (transform.position, Vector3.up, out hit , Mathf.Infinity, oceanLayer))
        {
            distanceToWave = -hit.distance;
        }
        Debug.Log(distanceToWave);

        return distanceToWave;
    }
}
