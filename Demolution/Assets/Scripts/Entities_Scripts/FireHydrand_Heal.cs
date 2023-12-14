using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant_Heal : MonoBehaviour{
	
	private Animator anim;
	public float crushableRange = 2f;
	public bool isCrushable = false;
	private Transform playerPos;
	public int crushStage = 0;
	
	//public AudioSource crushSFX;
	//public GameObject crushVFX;
	
	public int healAmt = 1;
	private bool canHeal = false;
	private bool isTouchingPlayer = false;
	private float timerLimit = 1f;
	private float theTimer = 0;
	
    void Start(){
		//crushVFX.SetActive(false);
		playerPos = GameObject.FindWithTag("Player").transform;
		anim = GetComponentInChildren<Animator>();
    }

	void Update(){
		float distToPlayerFeet = Vector2.Distance(transform.position, playerPos.position);
		if (distToPlayerFeet <= crushableRange){isCrushable = true;}
		else {isCrushable = false;}
		
		if ((isCrushable)&&(crushStage < 2)){
			if ((Input.GetAxis("Attack_Stomp") > 0)||(Input.GetAxis("Attack_Kick") > 0)){
				crushStage++;
				CrushMe();
			}
		}
	}

	void FixedUpdate(){
		if (canHeal){
			theTimer+= 0.01f;
			if (theTimer >= timerLimit){
				theTimer= 0;
				if (isTouchingPlayer){
				GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>().playerGetHealth(healAmt);
				}
			} 
		}
	}

    public void CrushMe(){
		//crushSFX.Play();
		//crushVFX.SetActive(true);
		if (crushStage==0){
			anim.SetBool("isBanged", false);
			anim.SetBool("isBroken", false);
			anim.SetBool("isDead", false);
		}
		else if (crushStage==1){
			anim.SetBool("isBanged", true);
			anim.SetBool("isBroken", false);
			anim.SetBool("isDead", false);
		}
		else{
			anim.SetBool("isBanged", false);
			anim.SetBool("isBroken", true);
			anim.SetBool("isDead", false);
			canHeal = true;
			StartCoroutine(runOutOfWater());
		}
    }
	
	public void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag=="Player"){
			isTouchingPlayer = true;
		} 
	}
	
	public void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag=="Player"){
			isTouchingPlayer = false;
		} 
	}
	
	
	IEnumerator runOutOfWater(){
		yield return new WaitForSeconds(10f);
		canHeal = false;
		anim.SetBool("isBanged", false);
		anim.SetBool("isBroken", false);
		anim.SetBool("isDead", true);
	}
	
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, crushableRange);
	}
}
