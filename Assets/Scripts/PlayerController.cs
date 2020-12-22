using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _turnSpeed = 2f;
    
    CharacterController _characterController;
    public bool IsGrounded;

    void Awake() => _characterController = GetComponent<CharacterController>();

    void FixedUpdate()
    {
        //        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(0, 0, vertical);
        Vector3 movement = transform.TransformDirection(direction) * _moveSpeed;
        transform.Rotate(0, _turnSpeed * Input.GetAxis("Horizontal"), 0);
        IsGrounded = _characterController.SimpleMove(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("onTriggerEnter");
        if (other.CompareTag("target"))
        {
            Debug.Log("change Parent");
            other.transform.parent = this.transform;
        }
    }
}
