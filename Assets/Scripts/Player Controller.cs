using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;

    [Header("Jump Settings")]
    private float jumpForce = 10f;
    private float maxChargeTime = 1.5f;
    private float currentCharge = 0f;
    private float maxJumpForce = 18f;

    private bool isGrounded = false;
    private bool landed = false;
    private bool isCharging = false;
    private bool hasBounced = false;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckGrounded();
        HandlePlayerDirection();
        HandleJump();

        // Freeze horizontal movement if landed
        if (landed)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void CheckGrounded()
    {
        // Slightly more reliable ground check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null && hit.normal.y > 0.7f;

        if (isGrounded)
        {
            hasBounced = false; // Reset bounce ability
        }
    }

    private void HandlePlayerDirection()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                rbSprite.flipX = false;
                facingRight = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                rbSprite.flipX = true;
                facingRight = false;
            }
        }
    }

    private void HandleJump()
    {
        // Start charging jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentCharge = 0f;
        }

        // While charging
        if (isCharging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeTime);
        }

        // Release jump
        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            isCharging = false;
            float jumpPower = (currentCharge / maxChargeTime) * maxJumpForce;

            // Unfreeze X to allow movement
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            Vector2 jumpDirection = new Vector2(facingRight ? 1f : -1f, 1f).normalized;
            rb.velocity = jumpDirection * jumpPower;
            landed = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.6f);
    }
}