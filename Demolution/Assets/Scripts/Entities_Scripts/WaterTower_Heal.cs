using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower_Heal : MonoBehaviour{
	
	private Animator anim;
	public float crushableRange = 2f;
	private bool isCrushable = false;
	private Transform playerPos;
	public int crushStage = 0;
	
	//public AudioSource crushSFX;
	//public GameObject crushVFX;
	
	public int healAmt = 20;
	
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
			if ((Input.GetAxis("Attack_Stomp") > 0)
				||(Input.GetAxis("Attack_Kick") > 0)
				||((Input.GetAxis("Attack_PunchForward") > 0)&&(GameHandler_PlayerManager.playerLevel < 24))
			){
				crushStage++;
				CrushMe();
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
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>().playerGetHealth(healAmt);
			StartCoroutine(runOutOfWater());
		}
    }
	
	IEnumerator runOutOfWater(){
		GameHandler_PlayerManager.hydrating = true;
		yield return new WaitForSeconds(2f);
		anim.SetBool("isBanged", false);
		anim.SetBool("isBroken", false);
		anim.SetBool("isDead", true);
		GameHandler_PlayerManager.hydrating = false;
	}
	
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, crushableRange);
	}
}
