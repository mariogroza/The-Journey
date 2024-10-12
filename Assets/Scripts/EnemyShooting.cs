using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    float distance;
    public float range;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private float timer;
    public float coolDown;
    private void Update()
    {
       
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance <= range)
        {
            timer += Time.deltaTime;

            if (timer >= coolDown)
            {
                timer = 0;
                Shoot();
            }
        }

    }

    void Shoot()
    {
        Instantiate(bullet,bulletPos.position,Quaternion.identity); 
    }

}
