using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;
    private bool _isDead;


    // Update is called once per frame
    void Update()
    {
 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameController.Instance.PlayerHit();
            AudioManager.Instance.PlaySound(AudioManager.Sound.SoundName.EnemyHitPlayer);
            animator.SetBool("Alive", false);
            _isDead = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Island")
        {
            Debug.Log("landed on an island");
            //add points.

            // disable player movement.
        }  
    }
}
