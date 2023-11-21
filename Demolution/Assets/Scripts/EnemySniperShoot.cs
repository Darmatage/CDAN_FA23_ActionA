using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySniperShoot : MonoBehaviour {

      //public Animator anim;
       private float timeBtwShots;
       public float startTimeBtwShots = 2;
	   public Transform firePoint;
	   public Transform turretHub;
	   public Transform turretDefault;
       public GameObject projectile;

       //private Rigidbody2D rb;
       public Transform player;
       private Vector2 PlayerVect;

       //public int EnemyLives = 30;
       private Renderer rend;
       //private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;

	void Start () {
		//anim = GetComponentInChildren<Animator> ();
		//rb = GetComponent<Rigidbody2D> ();
		   
		Physics2D.queriesStartInColliders = false;

		player = GameObject.FindWithTag("PlayerCenter").transform;
		PlayerVect = player.transform.position;

		timeBtwShots = startTimeBtwShots;

		rend = GetComponentInChildren<Renderer> ();  
	}

	void Update () {
		
		float DistToPlayer = Vector3.Distance(transform.position, player.position);
		if ((player != null) && (DistToPlayer <= attackRange)) {

			//Timer for shooting projectiles
			if (timeBtwShots <= 0) {
				isAttacking = true;
				//anim.SetTrigger("Attack");
				Instantiate (projectile, firePoint.position, Quaternion.identity);
				timeBtwShots = startTimeBtwShots;
			} else {
				timeBtwShots -= Time.deltaTime;
				isAttacking = false;
			}
		}
	}

	//rotate turret based on player position:
	void FixedUpdate(){
		float DistToPlayer = Vector3.Distance(transform.position, player.position);
		if ((player != null) && (DistToPlayer <= attackRange)) {
			//rotate turret towards player:
			Vector2 lookDir = ((Vector2)player.position - (Vector2)turretHub.position).normalized;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			float offset = 180f;
			turretHub.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
		} else {
			//rotate turret to forward (when player is not in range, or dead):
			turretHub.rotation = Quaternion.Euler(Vector3.forward);
		}
	}


	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			//EnemyLives -= 2;
			StopCoroutine("HitEnemy");
			StartCoroutine("HitEnemy");
		}
	}

	IEnumerator HitEnemy(){
              // color values are R, G, B, and alpha, each divided by 100
              rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              /*
			  if (EnemyLives < 1){
                     //gameControllerObj.AddScore (5);
                     Destroy(gameObject);
              }
			  
              else*/ yield return new WaitForSeconds(0.5f);
              rend.material.color = Color.white;
       }

      //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}
