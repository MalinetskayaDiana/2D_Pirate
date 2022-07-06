using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public float startHealth;
    [SerializeField] private float iFlamesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public Color colorFirst;
    public float currentHealth { get; private set; }
    public AudioSource Pain;
    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        colorFirst = spriteRend.color;
        currentHealth = startHealth;
    }

    public void TakeHit(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startHealth);
        Pain.Play();

        if (currentHealth <= 0)
        {
            animator.SetBool("dead", true);
            Destroy(gameObject);
        }
        if (currentHealth > 0)
        {
            StartCoroutine(Invunerability());
        }
    }

    public void GetHealth(float bonusHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth + bonusHealth, 0, startHealth);
        if (currentHealth >= startHealth)
        {
            currentHealth = startHealth;
        }
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFlamesDuration / (numberOfFlashes * 2));
            spriteRend.color = colorFirst;
            yield return new WaitForSeconds(iFlamesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
    /*public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeHit(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetHealth(1);
        }

    }*/
}
