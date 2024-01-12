using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      public static int playerHealth = 100;
      public int StartPlayerHealth = 100;
      public GameObject healthText;
	  
	  public GameObject healthAddText;
	  public GameObject healthMinusText;
	  
	  public GameObject islandNumText;

      public static int playerScore = 0;
      public GameObject tokensText;

      public bool isDefending = false;
	  public bool inWater = true;

      public static bool stairCaseUnlocked = false;
      //this is an example of a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;
      public static string lastLevelDied;  //allows replaying the Level where you died

      public static int levelNumber = 1;
      public static bool fireballUnlocked = false;
      public static bool doubleJumpUnlocked = false;

	void Start(){
		healthAddText.SetActive(false);
		healthMinusText.SetActive(false);
		  
		player = GameObject.FindWithTag("Player");
		sceneName = SceneManager.GetActiveScene().name;
		//if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
			playerHealth = StartPlayerHealth;
		//}
		updateStatsDisplay();
	}

	/*
	//moved to GameHandler_PlayerManager:
      public void playerGetTokens(int newTokens){
            playerScore += newTokens;
            updateStatsDisplay();
      }
	  */

	public void playerGetHit(int damage){
		if ((isDefending == false)||(inWater == false)){
			playerHealth -= damage;
			if (playerHealth >=0){
				updateStatsDisplay();
				Text healthMinusTextTemp = healthMinusText.GetComponent<Text>();
				healthMinusTextTemp.text = "-" + damage;
				StartCoroutine(TextScale(healthMinusText));
			}
			if (damage >= 3){
				player.GetComponent<PlayerMove>().playerHurtAnim(); //play GetHit animation for big weapons
			}
		}

		if (playerHealth > StartPlayerHealth){
			playerHealth = StartPlayerHealth;
			updateStatsDisplay();
		}

		if (playerHealth <= 0){
			playerHealth = 0;
			updateStatsDisplay();
			playerDies();
		}
	}


	public void playerGetHealth(int bonus){
		playerHealth += bonus;
		if ((playerHealth >=0)&&(playerHealth < StartPlayerHealth)){
			updateStatsDisplay();
			Text healthAddTextTemp = healthAddText.GetComponent<Text>();
			healthAddTextTemp.text = "+" + bonus;
			StartCoroutine(TextScale(healthAddText));
		}

		if (playerHealth > StartPlayerHealth){
			playerHealth = StartPlayerHealth;
			updateStatsDisplay();
		}

		if (playerHealth <= 0){
			playerHealth = 0;
			updateStatsDisplay();
			playerDies();
		}
	}


      public void updateStatsDisplay(){
            Text healthTextTemp = healthText.GetComponent<Text>();
            healthTextTemp.text = "" + playerHealth;

			string sceneNum = "0";
			if (sceneName =="Level1"){sceneNum="1";}
			else if (sceneName =="Level2"){sceneNum="2";}
			else if (sceneName =="Level3"){sceneNum="3";}
			else if (sceneName =="Level4"){sceneNum="4";}
			else if (sceneName =="Level5"){sceneNum="5";}
			else {sceneNum="X";}
			
			Text islandNumTextTemp = islandNumText.GetComponent<Text>();
			islandNumTextTemp.text = "#" + sceneNum;

            Text tokensTextTemp = tokensText.GetComponent<Text>();
            tokensTextTemp.text = "" + playerScore;
      }

      public void playerDies(){
            //player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            lastLevelDied = sceneName;       //allows replaying the Level where you died
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            player.GetComponent<PlayerMove>().isAlive = false;
            player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
			BGSoundScript.Instance.SwitchToMenuMusic();
            SceneManager.LoadScene("EndLose");
      }

	public void StartGame() {
		//BGSoundScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
		
		SceneManager.LoadScene("Level1");
		BGSoundScript.Instance.SwitchToGameMusic();
	}

	// Return to MainMenu
	public void RestartGame() {
		// Reset all static variables here, for new games:
		playerHealth = StartPlayerHealth;
		playerScore = 0;
		CameraFollow_Grow.camSize = 5f;
		GameHandler_PlayerManager.pointsToLevel1 = 0;
		GameHandler_PlayerManager.pointsToLevel2 = 10;
		GameHandler_PlayerManager.playerLevel = 1;
		GameHandler_PlayerManager.playerSize = 1;
		PlayerMove.walkSpeed = 3.5f;
			
		Time.timeScale = 1f;
		GameHandler_PauseMenu.GameisPaused = false;
		BGSoundScript.Instance.SwitchToMenuMusic();
		SceneManager.LoadScene("MainMenu");
	}

      // Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
			BGSoundScript.Instance.SwitchToGameMusic();
            SceneManager.LoadScene(lastLevelDied);
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
	  
	  
	  IEnumerator TextScale(GameObject theText){
		  //make text visible
		  theText.SetActive(true);
		  yield return new WaitForSeconds(0.5f);
		  //make text not visible
		  theText.SetActive(false);
	  }
	  
}
