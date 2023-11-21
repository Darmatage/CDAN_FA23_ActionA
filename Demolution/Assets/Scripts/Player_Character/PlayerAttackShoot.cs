using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackShoot : MonoBehaviour
{

    //public Animator animator;
    public Transform firePoint;
    public Transform fireDirectionDefault;
    private Vector2 mousePos;
    private Camera cam;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
      public float attackRate = 2f;
    private float nextAttackTime = 0f;

    Vector2 fwd;

   // private bool mouseBehindPlayer = false;

    void Start()
    {
        //animator = gameObject.GetComponentInChildren<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Time.time >= nextAttackTime)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            if (Input.GetAxis("Attack_Fireball") > 0)
            {
                playerFire();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void playerFire()
    {
        //animator.SetTrigger ("Fire");
        //Vector2 fwd = (firePoint.position - this.transform.position).normalized;
        if (((mousePos.x > firePoint.position.x)&&(gameObject.GetComponent<PlayerMove>().FaceRight)) ||
            ((mousePos.x < firePoint.position.x) && (!gameObject.GetComponent<PlayerMove>().FaceRight)))
        {
            fwd = (mousePos - (Vector2)firePoint.position).normalized;
        }
        else { 
            fwd = ((Vector2)fireDirectionDefault.position - (Vector2)firePoint.position).normalized; 
        }


        GameObject projectileX = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectileX.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
    }
}