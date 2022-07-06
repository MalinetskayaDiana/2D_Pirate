using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedShip : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 9, true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            rb.gravityScale = 0f;
        }
    }
}
