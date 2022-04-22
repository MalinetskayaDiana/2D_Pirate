using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionXP : MonoBehaviour
{
    public float collisionBonus;
    public string collisionTag;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == collisionTag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.GetHealth(collisionBonus);
            Destroy(gameObject);
        }
    }
}
