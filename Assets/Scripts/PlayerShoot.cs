using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.2f;
    public Transform firingPoint;
    public Transform firingUp;
    public GameObject bullet;

    [HideInInspector]public float timeUntilFire;
    PlayerMovement pm;
   
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && timeUntilFire < Time.time)
        {
            Shoot();
            timeUntilFire = Time.time + fireRate;
            
        }
    }
    void Shoot()
    {
        if (!pm.isFacingUp)
        {
            float angle = pm.isFacingRight ? 0f : 180f;
            Instantiate(bullet, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        }
        else
        {
            Instantiate(bullet, firingUp.position, Quaternion.Euler(new Vector3(0f, 0f, 90)));
        }
        

    }
    
}
