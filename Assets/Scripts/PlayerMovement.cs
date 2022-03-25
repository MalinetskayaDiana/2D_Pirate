using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    float sX, sY;
    private Rigidbody2D rb;
    float horizontalMove = 0f;
    private bool FacingRight = true;
    public bool isGround = false;
    [Header("Player Movement Settings")]
    public float runSpeed = 7f;
    public float jumpForce = 5f;
    [Range(-5f, 5f)] public float checkGroundOffSetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;
    [Header("Sound Settings")]
    public AudioSource JimpSound;
    public AudioSource GameOverSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sX = transform.position.x;
        sY = transform.position.y;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (horizontalMove < 0 && FacingRight)
        {
            Flip();
        }
        else if (horizontalMove > 0 && !FacingRight)
        {
            Flip();
        }

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            JimpSound.Play();
        }

        if (isGround == false)
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }

    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * 10f, rb.velocity.y);
        rb.velocity = targetVelocity;
        CheckGround();
    }
    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 tScale = transform.localScale;
        tScale.x *= -1;
        transform.localScale = tScale;
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY), checkGroundRadius);
        if (collider.Length > 1)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DeadZone")
        {
            GameOverSound.Play();
            transform.position = new Vector3(sX, sY, transform.position.z);
        }

        if (collision.gameObject.tag =="Platform")
        {
            this.transform.parent = collision.transform;
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            sX = transform.position.x;
            sY = transform.position.y;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
