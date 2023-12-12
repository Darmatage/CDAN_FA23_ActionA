using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSway : MonoBehaviour{
 
	private Animator anim;
	//public AudioSource rustleSFX;
	//public AudioSource crushSFX;
	
	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	//play sway animation when player walks past tree
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			
			if (other.gameObject.transform.position.x < transform.position.x){
				anim.SetTrigger("Sway");
			} else {
				anim.SetTrigger("SwayLeft");
			}
			
			//if (GameHandler_PlayerManager.playerSize > 5){
				//anim.SetTrigger("Sway");
			//}
			//rustleSFX.Play();
		}
	}

	public void CrushTree(){
		int randNum = Random.Range(1,4);
		
		if (randNum == 1){anim.SetTrigger("Crush1");}
		else if (randNum == 2){anim.SetTrigger("Crush2");}
		else {anim.SetTrigger("Crush3");}
		
		//crushSFX.Play();
	}

}


