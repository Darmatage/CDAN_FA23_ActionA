using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler_PlayerManager : MonoBehaviour{
	
	private Transform waterLine;
	private Transform playerBottom;
	private float timer = 0f;
	public float timeToDehydrate = 2f;
	
    void Start(){
        waterLine = GameObject.FindWithTag("WaterLine").transform;
		playerBottom = GameObject.FindWithTag("PlayerBottom").transform;
    }

	//This script manages player health
    void FixedUpdate(){
		//if the player is above water
        if (playerBottom.position.y > waterLine.position.y){
			gameObject.GetComponent<GameHandler>().inWater = false;
			timer += 0.01f;
			//if timeToDehydrate has passed
			if (timer >= timeToDehydrate){	
				Debug.Log("Player Lost 1 health due to dehydration");
				gameObject.GetComponent<GameHandler>().playerGetHit(1);
				timer = 0f;
			}
		} else {
			gameObject.GetComponent<GameHandler>().inWater = true;
		}
    }
	
	
	
}


