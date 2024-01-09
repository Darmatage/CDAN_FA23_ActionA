using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterHealing : MonoBehaviour{

	private int healBonus = 4;
	public bool isHealing = false;
	public float timeToHeal = 1f;
	private float healTimer = 0f;
	private GameHandler ghandler;
	
	void Start(){
		ghandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
	}

    void FixedUpdate(){
        if (isHealing == true){
			healTimer += 0.01f;
			if (healTimer >= timeToHeal){
				ghandler.playerGetHealth(healBonus);
				healTimer = 0f;
			}
		}
    }
	
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			GameHandler_PlayerManager.hydrating = true;	
		}
	}
	
	public void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			isHealing = true;	
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			isHealing = false;	
			GameHandler_PlayerManager.hydrating = false;
		}
	}
	
}
