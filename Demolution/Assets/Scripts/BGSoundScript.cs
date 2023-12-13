using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BGSoundScript : MonoBehaviour {

	public GameObject MusicMenu;
	public GameObject MusicGameplay;

	private static BGSoundScript instance = null;

	public static BGSoundScript Instance{
		get {return instance;}
	}

	void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Start(){
		Scene thisScene = SceneManager.GetActiveScene();
        string sceneName = thisScene.name;
		
		if ((sceneName=="MainMenu")||(sceneName=="Credits")||(sceneName=="EndWin")||(sceneName=="EndLose")){
			MusicMenu.SetActive(true);
			MusicGameplay.SetActive(false);
		}
		else {
			MusicMenu.SetActive(false);
			MusicGameplay.SetActive(true);
		}
	}
	
	public void SwitchToMenuMusic(){
		MusicMenu.SetActive(true);
		MusicGameplay.SetActive(false);
	}
	
	public void SwitchToGameMusic(){
		MusicMenu.SetActive(false);
		MusicGameplay.SetActive(true);
	}
	
	
	
} 