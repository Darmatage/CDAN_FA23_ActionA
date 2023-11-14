using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

	public GameObject gameHandler;
	//public playerVFX playerPowerupVFX;
	public bool isHealthPickUp = true;
	public bool isSpeedBoostPickUp = false;

	/*
	public int healthBoost = 50;
	public float speedBoost = 2f;
	public float speedTime = 2f;
	*/

	void Start(){
		gameHandler = GameObject.FindWithTag("GameHandler");
		//playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
	}

	public void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player"){
			GetComponent<Collider2D>().enabled = false;
			//GetComponent< AudioSource>().Play();
			StartCoroutine(DestroyThis());
			gameHandler.GetComponent<GameHandler_PlayerManager>().playerAddScore(1);
			
			/*
                  if (isHealthPickUp == true) {
                        gameHandler.GetComponent<GameHandler>().playerGetHit(healthBoost * -1);
                        //playerPowerupVFX.powerup();
                  }
				
                  if (isSpeedBoostPickUp == true) {
                        other.gameObject.GetComponent<PlayerMove>().speedBoost(speedBoost, speedTime);
                        //playerPowerupVFX.powerup();
                  }
			*/
		}
	}

	IEnumerator DestroyThis(){
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}

}