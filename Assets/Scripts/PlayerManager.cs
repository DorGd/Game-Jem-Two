using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;
    private bool _isDead = false;
    private bool _saved = false;
    private Vector3 validDirection = Vector3.up;  // What you consider to be upwards
    [SerializeField] private float contactThreshold = 15f;          // Acceptable difference in degrees

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
            this.gameObject.SetActive(false);        }
    }

    private void OnCollisionStay(Collision collision)
    {
        bool topCollision = false;
        if (collision.gameObject.tag == "Island" && !_saved )
        {
            if (rigidBody.velocity.magnitude <= 0.1f)
            {
                for (int k = 0; k < collision.contacts.Length; k++)
                {
                    if (Vector3.Angle(collision.contacts[k].normal, validDirection) <= contactThreshold)
                    {
                        topCollision = true;
                       
                    }
                }
                if (topCollision)
                {
                    // Collided with a surface facing mostly upwards
                    Debug.Log("landed on an island");
                    //add points.
                    GameController.Instance.AddScore(1);
                    AudioManager.Instance.PlaySound(AudioManager.Sound.SoundName.PlayerSaved);
                    _saved = true;
                }
            }
        }  
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Island" && _saved)
        {
            GameController.Instance.AddScore(-1);
            _saved = false;
        }
    }
}
