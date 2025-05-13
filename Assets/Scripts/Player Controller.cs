using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;

    [Header("Jump Settings")]
    [SerializeField] private float maxChargeTime = 1.5f;
    [SerializeField] private float maxJumpForce = 18f;

    [Header("Gravity Settings")]
    [SerializeField] private float fallMultiplier = 3f; // gravity multiplier when falling down
    [SerializeField] private float lowJumpMultiplier = 2f; // gravity multiplier when jump is released early
    [SerializeField] private float defaultGravityScale = 1f;

    private float currentCharge = 0f;

    private bool isGrounded = false;
    private bool isCharging = false;
    private bool facingRight = true;

    [SerializeField] private Slider chargeSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        rb.gravityScale = defaultGravityScale;
    }

    private void Update()
    {
        CheckGrounded();
        HandlePlayerDirection();
        HandleJump();
        chargeSlider.value = currentCharge;

        // Debug velocity each frame to observe horizontal component change
        Debug.Log($"Velocity: {rb.velocity}");
    }

    private void FixedUpdate()
    {
        ApplyBetterJumpPhysics();
    }
    private void ApplyBetterJumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            // Falling: increase gravity to fall faster
            rb.gravityScale = defaultGravityScale * fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // Short jump: increase gravity to fall faster if jump button released early
            rb.gravityScale = defaultGravityScale * lowJumpMultiplier;
        }
        else
        {
            // Normal gravity scale
            rb.gravityScale = defaultGravityScale;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
    }

    private void HandlePlayerDirection()
    {
        // Allow changing facing direction only when grounded and not charging jump
        if (isGrounded && !isCharging)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                facingRight = true;
                rbSprite.flipX = false;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                facingRight = false;
                rbSprite.flipX = true;
            }
        }
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentCharge = 0f;
        }

        if (isCharging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeTime);
        }

        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            isCharging = false;
            float jumpPower = (currentCharge / maxChargeTime) * maxJumpForce;

            Vector2 jumpDirection = new Vector2(facingRight ? 1f : -1f, 2f).normalized;
            Vector2 jumpVelocity = jumpDirection * jumpPower;

            Debug.Log($"Jumping with power {jumpPower:F2} in direction {jumpDirection}");

            rb.velocity = new Vector2(jumpVelocity.x, jumpVelocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.55f);
    }
}

