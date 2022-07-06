using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public int silverCoin, goldenCoin;

    float sX, sY;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    float horizontalMove;
    private bool FacingRight = true;
    public bool isGround = false;
    public bool isBonusJump = false;
    public bool isWater = false;
    [Header("Player Movement Settings")]
    public float runSpeed;
    public float jumpForce;
    public float jumpForceStart;
    public float jumpForceBonus;
    public float jumpBonusTime;
    public float jumpBonusTimeMax;
    /*public float underWaterTime;
    public float underWaterTimeMax;*/
    [Range(-5f, 5f)] public float checkGroundOffSetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;
    [Header("Sound Settings")]
    public AudioSource JimpSound;
    public AudioSource GameOverSound;
    public AudioSource CoinsSound;
    public AudioSource CheckpointSound;
    public AudioSource PotionsSound;
    [Header("Effects Settings")]
    public GameObject jumpPosionEffect;
    public GameObject healthPosionEffect;
    public GameObject checkpointEffect;
    public GameObject goldCoinEffect;
    public GameObject silverCoinEffect;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sX = transform.position.x;
        sY = transform.position.y;
        jumpForceStart = jumpForce;
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

        if (jumpBonusTime > 0)
        {
            Physics2D.IgnoreLayerCollision(6, 8, true);
            isBonusJump = true;
            /*spriteRend.color = new Color(0.15f, 0.95f, 0.25f, 0.8f);*/
            jumpForce = jumpForceBonus;
            jumpBonusTime -= Time.deltaTime;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(6, 8, false);
            /*spriteRend.color = new Color(1, 1, 1, 1);*/
            jumpForce = jumpForceStart;
            isBonusJump = false;
        }

        /*if (isWater == true)
        {
            if (underWaterTime > 0)
            {
                underWaterTime -= Time.deltaTime;
            }
            else
            {
                GameOverSound.Play();
                transform.position = new Vector3(sX, sY, transform.position.z);
            }
        }*/
    }
    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
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

    private void JumpBonus()
    {
        jumpBonusTime = jumpBonusTimeMax;
    }

    /*private void WaterTime()
    {
        underWaterTime = underWaterTimeMax;
    }*/
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

        if (collision.gameObject.tag == "HealthBonus")
        {
            Instantiate(healthPosionEffect, transform.position, Quaternion.identity);
            PotionsSound.Play();
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            Instantiate(checkpointEffect, transform.position, Quaternion.identity);
            CheckpointSound.Play();
            sX = transform.position.x;
            sY = transform.position.y;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "JumpBonus")
        {
            Instantiate(jumpPosionEffect, transform.position, Quaternion.identity);
            PotionsSound.Play();
            JumpBonus();
            Destroy(collision.gameObject);
        }

       /* if (collision.gameObject.tag == "Zone")
        {
            WaterTime();
        }*/
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

        if (collision.tag == "SilverCoin")
        {
            Instantiate(silverCoinEffect, transform.position, Quaternion.identity);
            CoinsSound.Play();
            silverCoin++;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "GoldenCoin")
        {
            Instantiate(goldCoinEffect, transform.position, Quaternion.identity);
            CoinsSound.Play();
            goldenCoin++;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Water")
        {
            isWater = true;

        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            isWater = false;

        }
    }*/

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        
    }*/

    /*private IEnumerator GreenBonusLastTime()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0.15f, 0.95f, 0.24f, 0.8f);
            yield return new WaitForSeconds(FlamesDuration / (numberOf_Flashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(FlamesDuration / (numberOf_Flashes * 2));
        }
    }*/
}
