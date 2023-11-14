using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler_PlayerManager : MonoBehaviour{
	
	//hydration variables
	private Transform waterLine;
	private Transform playerBottom;
	private float timer = 0f;
	public float timeToDehydrate = 2f;
	
	//growth static variables (need to live here, so do not disappear when player dies)
	public static int pointsToLevel1 = 0;
	public static int pointsToLevel2 = 10;
	public static int playerLevel = 1;
	public static float playerSize = 1;
	
	
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
	
	public void playerAddScore(int addScore){
            GameHandler.playerScore += addScore;
            gameObject.GetComponent<GameHandler>().updateStatsDisplay();
			GameObject.FindWithTag("Player").GetComponent<Player_Grow>().UpdatePlayerSize();
      }
	
	
	
}


