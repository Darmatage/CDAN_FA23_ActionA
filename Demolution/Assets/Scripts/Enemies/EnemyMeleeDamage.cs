using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {
	private Renderer rend;
	private Animator anim;
	public GameObject healthLoot;
	public int maxHealth = 100;
	public int currentHealth;
	   
	public GameObject smokeTrailVFX;

	public bool isCopter = false;
	public bool isMech = false;

	void Start(){
		rend = GetComponentInChildren<Renderer> ();
		if (isMech){
			anim = GetComponentInChildren<Animator>();
		}
		currentHealth = maxHealth;
			  
		smokeTrailVFX.SetActive(false);
	}

	public void TakeDamage(int damage){
		currentHealth -= damage;
		Debug.Log("I got hit" + gameObject.name);
		rend.material.color = new Color(1.5f, 0.5f, 0.5f, 1f);
		StartCoroutine(ResetColor());
		//anim.SetTrigger ("Hurt");
		if (currentHealth <= 0){
			Die();
		}
	}

	void Die(){
		if (isCopter){
			gameObject.GetComponent<EnemyUpDown_Copter>().enabled = false;
			gameObject.GetComponent<EnemyMoveShoot_Copter>().enabled = false;	
			transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime); 
		}
		GetComponent<Rigidbody2D>().gravityScale = 1;
		Instantiate (healthLoot, transform.position, Quaternion.identity);
		if (isMech){
			anim.SetBool("broken", true);
		}
		smokeTrailVFX.SetActive(true);
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Rigidbody2D>().gravityScale = 1;
		StartCoroutine(Death());
	}

	IEnumerator Death(){
		yield return new WaitForSeconds(3f);
		Debug.Log("You Killed a baddie. You deserve loot!");
		Destroy(gameObject);
	}

	IEnumerator ResetColor(){
		yield return new WaitForSeconds(0.5f);
		rend.material.color = Color.white;
	}
}