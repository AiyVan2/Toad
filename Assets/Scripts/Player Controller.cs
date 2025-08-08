using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private Animator animator;

    [Header("Jump Settings")]
    [SerializeField] private float maxChargeTime = 1.5f;
    [SerializeField] private float maxJumpForce = 18f;

    [Header("Gravity Settings")]
    [SerializeField] private float fallMultiplier = 3f;
    [SerializeField] private float lowJumpMultiplier = 2f; 
    [SerializeField] private float defaultGravityScale = 1f;

    private float currentCharge = 0f;

    private bool isGrounded = false;
    private bool isCharging = false;
    private bool facingRight = true;

    // Mobile control flags
    private bool lookLeftPressed = false;
    private bool lookRightPressed = false;

    [SerializeField] private Slider chargeSlider;

    public SoundEffects soundeffects;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.gravityScale = defaultGravityScale;


    }

    private void Update()
    {
        CheckGrounded();
        HandlePlayerDirection();
        HandleJumpMobile();
        chargeSlider.value = currentCharge;

        if (isGrounded)
        {
            animator.Play("Froggo Idle");
        } else if(rb.velocity.y < 0)
        {
            animator.Play("Froggo Fall");
        }
        else
        {
            animator.Play("Froggo Jump");
        }
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
        Vector2 boxSize = new Vector2(1f, 0.1f); 
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * 0.55f;

        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
    }

    private void HandlePlayerDirection()
    {
        if (isGrounded && !isCharging)
        {
            if (lookRightPressed)
            {
                facingRight = true;
                rbSprite.flipX = false;
            }
            else if (lookLeftPressed)
            {
                facingRight = false;
                rbSprite.flipX = true;
            }
        }
    }

    public void LookLeftDown() => lookLeftPressed = true;
    public void LookLeftUp() => lookLeftPressed = false;
    public void LookRightDown() => lookRightPressed = true;
    public void LookRightUp() => lookRightPressed = false;

    private void HandleJumpMobile()
    {
        if (isGrounded && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            if (touch.phase == TouchPhase.Began)
            {
                isCharging = true;
                currentCharge = 0f;
            }
            else if (touch.phase == TouchPhase.Stationary && isCharging)
            {
                currentCharge += Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeTime);
            }
            else if (touch.phase == TouchPhase.Ended && isCharging)
            {
                isCharging = false;
                float jumpPower = (currentCharge / maxChargeTime) * maxJumpForce;
                soundeffects.PlayJumpSound();

                Vector2 jumpDirection = new Vector2(facingRight ? 1f : -1f, 2.5f).normalized;
                Vector2 jumpVelocity = jumpDirection * jumpPower;

                rb.velocity = new Vector2(jumpVelocity.x, jumpVelocity.y);
            }
        }
    }


    
    private void OnDrawGizmosSelected()
    {
        Vector2 boxSize = new Vector2(1f, 0.1f);
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * 0.55f;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}

