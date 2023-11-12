using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveShoot : MonoBehaviour {

      //public Animator anim;
       public float speed = 2f;
       public float stoppingDistance = 4f; // when enemy stops moving towards player
       public float retreatDistance = 3f; // when enemy moves away from approaching player
       private float timeBtwShots;
       public float startTimeBtwShots = 2;
	   public Transform firePoint;
	   public Transform turretHub;
	   public Transform turretDefault;
       public GameObject projectile;

       private Rigidbody2D rb;
       public Transform player;
       private Vector2 PlayerVect;

       public int EnemyLives = 30;
       private Renderer rend;
       //private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       void Start () {
              Physics2D.queriesStartInColliders = false;
              scaleX = gameObject.transform.localScale.x;

              rb = GetComponent<Rigidbody2D> ();
              player = GameObject.FindWithTag("PlayerCenter").transform;
              PlayerVect = player.transform.position;

              timeBtwShots = startTimeBtwShots;

              rend = GetComponentInChildren<Renderer> ();
              //anim = GetComponentInChildren<Animator> ();

              //if (GameObject.FindWithTag ("GameHandler") != null) {
              // gameHander = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
              //}
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, player.position);
              if ((player != null) && (DistToPlayer <= attackRange)) {
                     // approach player
                     if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
                            transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
                            if (isAttacking == false) {
                                   //anim.SetBool("Walk", true);
                            }
                            //Vector2 lookDir = PlayerVect - rb.position;
                            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
                            //rb.rotation = angle;
                     }
                     // stop moving
                     else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
                            transform.position = this.transform.position;
                            //anim.SetBool("Walk", false);
                     }

                     // retreat from player
                     else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
                            transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
                            if (isAttacking == false) {
                                   //anim.SetBool("Walk", true);
                            }
                     }

                     //Flip enemy to face player direction. Wrong direction? Swap the * -1.
                     if (player.position.x > gameObject.transform.position.x){
                            gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                    } else {
                             gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                     }

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
					 
			//rotate turret towards player (this system works, but the FixedUpdate system is more robust):
			//turretHub.right = (player.position - turretHub.position) * -1;	 
		} else {
			//rotate turret to forward (when player is not in range, or dead):
			//turretHub.right = (turretDefault.position - turretHub.position) * -1;
			
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
		//if (collision.gameObject.tag == "bullet") {
		// EnemyLives -= 1;
		// StopCoroutine("HitEnemy");
		// StartCoroutine("HitEnemy");
		//}
		if (collision.gameObject.tag == "Player") {
			EnemyLives -= 2;
			StopCoroutine("HitEnemy");
			StartCoroutine("HitEnemy");
		}
	}

	IEnumerator HitEnemy(){
              // color values are R, G, B, and alpha, each divided by 100
              rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              if (EnemyLives < 1){
                     //gameControllerObj.AddScore (5);
                     Destroy(gameObject);
              }
              else yield return new WaitForSeconds(0.5f);
              rend.material.color = Color.white;
       }

      //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}
