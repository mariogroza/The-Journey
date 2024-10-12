using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 10f;
    public Rigidbody2D rb;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            anim.SetTrigger("impact");
            Destroy(gameObject,0.15f); 
        }   
    }
 
}
