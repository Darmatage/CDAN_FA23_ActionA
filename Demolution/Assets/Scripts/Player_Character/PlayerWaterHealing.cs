using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterHealing : MonoBehaviour{

	public int healBonus = 2;
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
	
	
	public void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			isHealing = true;	
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			isHealing = false;	
		}
	}
	
	
	
}
