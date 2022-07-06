using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBonus : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    [SerializeField] private float FlamesDuration;
    [SerializeField] private int numberOf_Flashes;
    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "JumpBonus")
        {
            spriteRend.color = new Color(0.15f, 0.95f, 0.25f, 0.8f);
        }
    }
    private IEnumerator GreenBonusLastTime()
    {
        for (int i = 0; i < numberOf_Flashes; i++)
        {
            spriteRend.color = new Color(0.15f, 0.95f, 0.24f, 0.8f);
            yield return new WaitForSeconds(FlamesDuration / (numberOf_Flashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(FlamesDuration / (numberOf_Flashes * 2));
        }
    }
}
