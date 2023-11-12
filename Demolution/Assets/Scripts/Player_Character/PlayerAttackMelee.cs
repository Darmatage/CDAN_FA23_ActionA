using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      private Animator anim;
      public Transform attackPt_PunchForward, attackPt_PunchUp, attackPt_kick, attackPt_stomp;
	  
      public float attackRange = 0.5f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      public LayerMask enemyLayers;
	  private Collider2D[] hitEnemies;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update(){
		if (Time.time >= nextAttackTime){
			//if (Input.GetKeyDown(KeyCode.Space))
			if (Input.GetAxis("Attack_PunchForward") > 0){
				Attack("punchForward");
				nextAttackTime = Time.time + 1f / attackRate;
			}
			if (Input.GetAxis("Attack_PunchUp") > 0){
				Attack("punchUp");
				nextAttackTime = Time.time + 1f / attackRate;
			}
			if (Input.GetAxis("Attack_Kick") > 0){
				Attack("kick");
				nextAttackTime = Time.time + 1f / attackRate;
			}
			if (Input.GetAxis("Attack_Stomp") > 0){
				Attack("stomp");
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
	}

	void Attack(string move){
		if (move == "punchForward"){
			anim.SetTrigger ("attack_punchForward");
			hitEnemies = Physics2D.OverlapCircleAll(attackPt_PunchForward.position, attackRange, enemyLayers);
		} else if (move == "punchUp"){
			anim.SetTrigger ("attack_punchUp");
			hitEnemies = Physics2D.OverlapCircleAll(attackPt_PunchUp.position, attackRange, enemyLayers);
		} else if (move == "kick"){
			anim.SetTrigger ("attack_kick");
			hitEnemies = Physics2D.OverlapCircleAll(attackPt_kick.position, attackRange, enemyLayers);
		} else {
			anim.SetTrigger ("attack_stomp");
			hitEnemies = Physics2D.OverlapCircleAll(attackPt_stomp.position, attackRange, enemyLayers);
		}
		
		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("We hit " + enemy.name);
			enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
		}
	}

	//NOTE: to help see the attack sphere in editor:
	void OnDrawGizmosSelected(){
		if (attackPt_PunchForward == null) {return;}
		Gizmos.DrawWireSphere(attackPt_PunchForward.position, attackRange);
		
		if (attackPt_PunchUp == null) {return;}
		Gizmos.DrawWireSphere(attackPt_PunchUp.position, attackRange);
		
		if (attackPt_kick == null) {return;}
		Gizmos.DrawWireSphere(attackPt_kick.position, attackRange);
		
		if (attackPt_stomp == null) {return;}
		Gizmos.DrawWireSphere(attackPt_stomp.position, attackRange);
	}
	
}
