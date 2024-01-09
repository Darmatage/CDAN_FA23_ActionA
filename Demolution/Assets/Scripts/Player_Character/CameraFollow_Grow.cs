using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraFollow_Grow : MonoBehaviour{

	//camera follow variables:
	public Transform targetTop;
	public Transform targetBase;
	  
	//public float camOffsetY = 1f;
	public float camSpeed = 4.0f;
	  
	//camera scale variables:
	public static float camSize = 5f;
	public float camSizeLast;
	public float newCamSize;
	//float camScaleSpeed = 10f;
	  
	void Start(){
		gameObject.GetComponent<Camera>().orthographicSize = camSize;
		
		Debug.Log("camera sizing: targetTop = " + targetTop.position.y 
					+ ", target bottom = " + targetBase.position.y 
					+ ", distance x1.02 = " + 
					((Mathf.Abs(targetTop.position.y) + Mathf.Abs(targetBase.position.y)) * 1.02));
	}

	void FixedUpdate () {
		//Vector2 camOffset = new Vector2(0, camOffsetY);
		//Vector2 camOffset = new Vector2(0, 0);
		//Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)target.position + camOffset, camSpeed * Time.fixedDeltaTime);
		Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)targetTop.position, camSpeed * Time.fixedDeltaTime);
		transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
    }
	  
	public void playerGrowCameraWide(){
		camSizeLast = camSize;
		//get the y-distance between the character head and base positions, and double it
		newCamSize = (Mathf.Abs(targetTop.position.y) + Mathf.Abs(targetBase.position.y)) * 1.02f; 
		if (newCamSize < 5){newCamSize = 5;}
		if (newCamSize > 10){newCamSize = 10;}
		Debug.Log("camera sizing: targetTop = " + targetTop.position.y 
					+ ", target bottom = " + targetBase.position.y 
					+ ", distance x1.02 = " + newCamSize);
		StartCoroutine(ResizeTime(camSizeLast, newCamSize, 1f));	
	}

	private IEnumerator ResizeTime(float oldSize, float newSize, float time){
		float elapsed = 0;
		while (elapsed <= time){
			elapsed += Time.deltaTime;
			float t = Mathf.Clamp01(elapsed / time);

			gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(oldSize, newSize, t);
			camSize = newCamSize;
			yield return null;
		}
	}
 
}

//Apply this script to a the camera that folows the player (instead of other camera follow 