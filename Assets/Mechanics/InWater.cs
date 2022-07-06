using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWater : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            rb.gravityScale = 0.000001f;
            rb.mass = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rb.gravityScale = 5;
        rb.mass = 2f;
    }
}
