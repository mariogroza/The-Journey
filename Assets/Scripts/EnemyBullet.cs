using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    private Animator anim;
    float rot;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Health>().health -= 25;
                other.gameObject.GetComponent<Health>().anim.Play("Player Hurt");
                Destroy(gameObject);
                
            }
        }
    }
}
