using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager : MonoBehaviour
{

    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private float radius = 5.0F;
    [SerializeField] private float power = 10.0F;
    [SerializeField] private GameObject rippleEffect;

 void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = -Vector3.one;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 1000f, waterLayer))
            {
                clickPosition = hit.point;
            }

            Debug.Log(clickPosition);
            Vector3 explosionPos = clickPosition;
            GameObject ripple = Instantiate(rippleEffect, clickPosition, Quaternion.Euler(-90, 0, 0));
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider col in colliders)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 0.0F);
            }
    
}
        

    }
}
