using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShootAttack : MonoBehaviour
{
  public Transform firePoint;
  public GameObject projectilePrefab;
  public float projectileSpeed = 10f;
  public float attackRate = 2f;
  private float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Time.time >= nextAttackTime)
      {
        if (Input.GetAxis("Attack") > 0)
        {
            playerFire();
            nextAttackTime = Time.time + 1f / attackRate;
        }
      }
    }

    void playerFire()
    {
      Vector2 fwd = (firePoint.position - this.transform.position).normalized;
      GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
      projectile.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
    }
}
