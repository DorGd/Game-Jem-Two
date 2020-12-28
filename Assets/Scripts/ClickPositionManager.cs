using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager : MonoBehaviour
{

    [SerializeField] private LayerMask waterLayer;
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
        }
        

    }
}
