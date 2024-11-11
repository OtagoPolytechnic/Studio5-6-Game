using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform sprite;
    public Transform firePoint;
    private float bulletSpeed = 50;

    public static float firerate = 0.5f;
    private float shootingInterval = 0;

    Vector2 lookDirection;
    float lookAngle;

    public void ShootingTest()
    {
        // Capture mouse position and calculate shooting direction
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Rotate firePoint towards mouse position
       firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        // Instantiate bullet clone and set its position and rotation
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        // Apply velocity to the bullet clone
        bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
    }

    void Update()
    {
        if(CatShop.instance != null && CatShop.instance.ShopMenuOpen)
        {
            return;
        }
        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        print("CAM " +cameraPos);
        lookDirection = new Vector2(cameraPos.x - transform.position.x, cameraPos.y - transform.position.y);
        print("DIR: " + lookDirection);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        print("ANGLE: " + lookAngle);
        sprite.rotation = Quaternion.Euler(0, 0, lookAngle);
        HandleShooting();
    }
    void HandleShooting()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval <= 0 && Input.GetMouseButton(0))
        {
            shootingInterval = firerate; 
            Shoot();
        }
    }

    private void Shoot()
    {
        if (PlayerHealth.hasShotgun)
        {
            //shoot 1+stacks(2) bullets in a cone infront of the player
            float shotAngle = 10f;
            for (int i = 0; i < PlayerHealth.bulletAmount + 1; i++)
            {
                firePoint.rotation = Quaternion.Euler(0, 0, lookAngle + shotAngle);
                GameObject bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, lookAngle));
                bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
                shotAngle -= 10f;
            }
        }
        else
        {
            GameObject bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, lookAngle));
            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        }
        // Play the duck shooting sound
        SFXManager.Instance.DuckShootSound(); 
    }
}