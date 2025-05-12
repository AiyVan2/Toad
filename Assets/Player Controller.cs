using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private float jumpForce = 10f;

    private bool isGrounded = false;
    bool isJumping = false;

    bool lastkeyboarddirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rbSprite = GetComponent<SpriteRenderer>();
    }

    private float maxChargeTime = 1.5f;
    private float currentCharge = 0f;
    private float maxJumpForce = 18f;
    private bool isCharging = false;
    private bool facingRight = true;

    void Update()
    {
        // Direction Flip (keep this)
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

        // Ground check
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));

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

            Vector2 jumpDirection = new Vector2(facingRight ? 1f : -1f, 1f).normalized;

            rb.velocity = jumpDirection * jumpPower;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1f);
    }
}