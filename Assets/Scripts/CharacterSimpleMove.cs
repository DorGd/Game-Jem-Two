using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSimpleMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;

    CharacterController _characterController;
    public bool IsGrounded;

    void Awake() => _characterController = GetComponent<CharacterController>();
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 movement = transform.TransformDirection(direction) * _moveSpeed;
        IsGrounded = _characterController.SimpleMove(movement);
    }
}