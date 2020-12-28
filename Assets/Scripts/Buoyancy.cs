using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Buoyancy : MonoBehaviour {
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 4.0f;

    private float forceFactor;
    private Vector3 floatForce;
    private LayerMask oceanLayer;

    private void Awake()
    {
        oceanLayer = LayerMask.GetMask("Water");
    }
    void FixedUpdate () {
        forceFactor = 1.0f - (getDistanceToWave() / floatThreshold);

        if (forceFactor > 0.0f) {
            floatForce = -Physics.gravity * GetComponent<Rigidbody> ().mass * (forceFactor - GetComponent<Rigidbody> ().velocity.y * waterDensity);
            floatForce += new Vector3 (0.0f, -downForce * GetComponent<Rigidbody> ().mass, 0.0f);
            GetComponent<Rigidbody> ().AddForceAtPosition (floatForce, transform.position);
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
        //Debug.Log(distanceToWave);

        return distanceToWave;
    }
}