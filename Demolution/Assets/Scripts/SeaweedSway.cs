using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedSway : MonoBehaviour{
 
	private Animator anim;
	public AudioSource seaweed1SFX;
	
	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	//play sway animation when player walks past tree
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			
			if (other.gameObject.transform.position.x < transform.position.x){
				anim.SetTrigger("SwayRight");
			} else {
				anim.SetTrigger("SwayLeft");
			}
			
			//if (GameHandler_PlayerManager.playerSize > 5){
				//anim.SetTrigger("Sway");
			//}
			//rustleSFX.Play();
		}
	}

}
