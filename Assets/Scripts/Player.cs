using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Colliders")]
    [SerializeField] BoxCollider2D feet;

    [Header("Config")]
    [SerializeField] AudioClip playerDeathClip;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 kickAfterDeath = new Vector2(0, 30f);

    bool isAlive = true;

    CapsuleCollider2D body;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    void Start()
    {
        body = GetComponent<CapsuleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        Jump();
        FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive) { return; }

        Die();
    }

    private void Run()
    {
        float axis = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(axis * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0f;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if(Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity = jumpVelocity;
        }
    }

    private void Die()
    {
        if(body.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            myRigidbody.sharedMaterial = null;

            AudioSource.PlayClipAtPoint(playerDeathClip, Camera.main.transform.position);
            myRigidbody.velocity = kickAfterDeath;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
