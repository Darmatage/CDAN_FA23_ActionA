using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Grow : MonoBehaviour{
	
//moved to GameHandler_PlayerManager:
	//public static int playerScore = 0;
	//public static int pointsToLevel1 = 0;
	//public static int pointsToLevel2 = 10;
	//public static int playerLevel = 1;
	//public static float playerSize = 1;
	
	//local variables
	public Animator anim1;
	public Animator anim2;
	public int pointsToNextLevel;
	public float playerSizeMultiplier = 1.02f;
	private float playerSizeNew;
	public GameObject ghostVFX; //a low-opacity copy of character art for growing effect
	//public AudioSource growSFX; //a sound for when the player grows, 2-seconds long
	
	//reference the camera that follows the player
	public CameraFollow_Grow myCamGrow;
	private float camGrowAmt = 0.2f;
	private int camTimerSmall = 0;
	private int camTimerBig = 0;
	public AudioSource roar1SFX;


	void Start(){
		gameObject.transform.localScale = new Vector3(
		GameHandler_PlayerManager.playerSize, 
		GameHandler_PlayerManager.playerSize, 
		GameHandler_PlayerManager.playerSize);
		ghostVFX.transform.localScale = new Vector3(1, 1, 1);
		ghostVFX.SetActive(false);
	}

    void Update(){
		//test buttons (real result should come from melee impact/ points accumulation)
		if ((Input.GetKeyDown("p"))&&(Input.GetKeyDown(KeyCode.LeftShift))){
			//size based on direct input (for testing):
			StartCoroutine(PlayerGrowers());
		}
		if ((Input.GetKeyDown("o"))&&(Input.GetKeyDown(KeyCode.LeftShift))){
			//Test size based on points accumulation:
			GameHandler.playerScore ++;
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_PlayerManager>().playerAddScore(1);
		}
		
    }
	
	//call this function whenever player gets a point:
	public void UpdatePlayerSize(){
		pointsToNextLevel = (GameHandler_PlayerManager.pointsToLevel1 + GameHandler_PlayerManager.pointsToLevel2);
		if (GameHandler.playerScore >= pointsToNextLevel){
			//update player size and level:
			GameHandler_PlayerManager.playerLevel ++;
			StartCoroutine(PlayerGrowers());
			roar1SFX.Play();
			
			//update two points records 
			GameHandler_PlayerManager.pointsToLevel1 = GameHandler_PlayerManager.pointsToLevel2;
			GameHandler_PlayerManager.pointsToLevel2 = pointsToNextLevel;
		}
	}

	//first of four grow functions, to manage the other three
	IEnumerator PlayerGrowers(){
		ghostVFX.SetActive(true);
		//ghostVFX.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
		playerSizeNew = GameHandler_PlayerManager.playerSize * playerSizeMultiplier;
		float ghostSizeNew = 1.1f * playerSizeMultiplier;
		float theTime = 0.5f;
		
		anim1.SetTrigger("Grow");
		anim2.SetTrigger("Grow");
		
		//growSFX.Play();
		StartCoroutine(PlayerGrowVFX(1f, ghostSizeNew, theTime));
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(PlayerGrowObj(GameHandler_PlayerManager.playerSize, playerSizeNew, theTime));
		yield return new WaitForSeconds(0.5f);
		PlayerGrowCamera();
		//growSFX.Stop();
	}

	IEnumerator PlayerGrowVFX(float oldSize, float newSize, float time){
		float ghostAlpha = 0;
		
		ghostVFX.SetActive(true);
		
		float elapsed1 = 0;
		while (elapsed1 <= time){
			elapsed1 += Time.deltaTime;
			float t = Mathf.Clamp01(elapsed1 / time);

			Vector3 baseGhostVect = new Vector3(oldSize, oldSize, oldSize);
			Vector3 newGhostVect = new Vector3(newSize, newSize, newSize);

			//fade-in the grow effect
			ghostVFX.transform.localScale = Vector3.Lerp(baseGhostVect, newGhostVect, t);
			
			SpriteRenderer ghostRend = ghostVFX.GetComponentInChildren<SpriteRenderer>();
			ghostRend.color = new Color(2.5f, 2.5f, 2.5f, (Mathf.Lerp(0f, 0.3f, t)));
			
			yield return null;
		}
	}

	IEnumerator PlayerGrowObj(float oldSize, float newSize, float time){
		//ghostVFX.SetActive(true);
		//ghostVFX.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
		
		float elapsed = 0;
		while (elapsed <= time){
			elapsed += Time.deltaTime;
			float t = Mathf.Clamp01(elapsed / time);

			Vector3 oldSizeVect = new Vector3(oldSize, oldSize, oldSize);
			Vector3 newSizeVect = new Vector3(newSize, newSize, newSize);
			bool facingRight = gameObject.GetComponent<PlayerMove>().FaceRight;
			if (!facingRight){
				oldSizeVect = new Vector3(oldSize * -1, oldSize, oldSize);
				newSizeVect = new Vector3(newSize * -1, newSize, newSize);
			} 

			//grow the player
			gameObject.transform.localScale = Vector3.Lerp(oldSizeVect, newSizeVect, t);
			
			//fade-out the grow effect
			SpriteRenderer ghostRend = ghostVFX.GetComponentInChildren<SpriteRenderer>();
			ghostRend.color = new Color(2.5f, 2.5f, 2.5f, (Mathf.Lerp(0.3f, 0f, t)));
			
			//reset
			GameHandler_PlayerManager.playerSize = playerSizeNew;
			yield return null;
		}
		//ghostVFX.transform.localScale = new Vector3(1, 1, 1);
		ghostVFX.SetActive(false);
		
		//increase the jumpForce
		gameObject.GetComponent<PlayerJump>().jumpForceGrow(); 
		gameObject.GetComponent<PlayerMove>().walkSpeedGrow();
	}

	void PlayerGrowCamera(){
		//grow the camera size every 5 player-size increases:
		camTimerSmall++;
		if (camTimerSmall >= 3){
			myCamGrow.playerGrowCameraWide();
			camTimerSmall = 0;
			camTimerBig ++;
			if (camTimerBig >= 2){
				//camGrowAmt = camGrowAmt * playerSizeMultiplier;
				//camGrowAmt = camGrowAmt * 1.2f;
				camTimerBig = 0;
			}
		}

		Debug.Log("Player grew to level " + GameHandler_PlayerManager.playerLevel + "and is now size " + GameHandler_PlayerManager.playerSize);
	}

}

//Apply this script to a player that has a duplicate art object, for the ghost effect 
//make sure the pivot is on the character art base, and the collider offset vertically upward to surround the art