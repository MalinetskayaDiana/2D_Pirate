using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothMove : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed = 1f;
    public Transform startPos;
    Vector3 nextPos;
    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
}
