using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushableObject : MonoBehaviour{
	
	public GameObject object_notCrushed;
	public GameObject object_crushed1;
	public GameObject object_crushed2;
	public GameObject object_crushed3;
	
	public float crushableRange = 2f;
	private bool isCrushable = false;
	private Transform playerPos;
	
	//public AudioSource crushSFX;
	//public GameObject crushVFX;
	
	public bool isCar = false;
	
    void Start(){
		object_notCrushed.SetActive(true);
        object_crushed1.SetActive(false);
		object_crushed2.SetActive(false);
		object_crushed3.SetActive(false);
		//crushVFX.SetActive(false);
		playerPos = GameObject.FindWithTag("Player").transform;
    }

	void Update(){
		float distToPlayerFeet = Vector2.Distance(transform.position, playerPos.position);
		if ((distToPlayerFeet <= crushableRange)&&(GameHandler_PlayerManager.playerLevel < 15)){
			isCrushable = true;
		}
		else {
			isCrushable = false;
		}
		
		if ((Input.GetAxis("Attack_Stomp") > 0)&&(isCrushable)){
			CrushMe();
		}
	}

    public void CrushMe(){
		//crushSFX.Play();
		//crushVFX.SetActive(true);
		
		int crushNum = Random.Range(0,3);
		if (crushNum ==0){object_crushed1.SetActive(true);}
		else if (crushNum ==1){object_crushed2.SetActive(true);}
		else {object_crushed3.SetActive(true);}
		
        object_notCrushed.SetActive(false);
		
		if (isCar == true){
			gameObject.GetComponent<Car_Movement>().isCrushed();
		}
    }
	
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, crushableRange);
	}
	
}