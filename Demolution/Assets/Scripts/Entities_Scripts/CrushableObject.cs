using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushableObject : MonoBehaviour{
	
	public GameObject object_notCrushed;
	public GameObject object_crushed;
	
	public float crushableRange = 2f;
	private bool isCrushable = false;
	private Transform playerPos;
	
	//public AudioSource crushSFX;
	//public GameObject crushVFX;
	
	public bool isCar = false;
	
    void Start(){
		object_notCrushed.SetActive(true);
        object_crushed.SetActive(false);
		//crushVFX.SetActive(false);
		playerPos = GameObject.FindWithTag("Player").transform;
    }

	void Update(){
		float distToPlayerFeet = Vector2.Distance(transform.position, playerPos.position);
		if (distToPlayerFeet <= crushableRange){isCrushable = true;}
		else {isCrushable = false;}
		
		if ((Input.GetAxis("Attack_Stomp") > 0)&&(isCrushable)){
			CrushMe();
		}
	}

    public void CrushMe(){
		//crushSFX.Play();
		//crushVFX.SetActive(true);
		
        object_notCrushed.SetActive(false);
		object_crushed.SetActive(true);
		
		if (isCar == true){
			gameObject.GetComponent<Car_Movement>().isCrushed();
		}
    }
	
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, crushableRange);
	}
	
}