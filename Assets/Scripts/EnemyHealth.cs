using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private Animator anim;
    [HideInInspector]public int score;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 10f;
            anim.SetTrigger("isShot");
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            Destroy(gameObject, 0.4f);
            score++;
        }
    }
}
