using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraFollow_Grow : MonoBehaviour{

	//camera follow variables:
      public GameObject target;
	  public float camOffsetY = 1f;
      public float camSpeed = 4.0f;
	  
	//camera scale variables:
	public float camSize = 5f;
	public float camSizeLast;
	private float newCamSize;
	float camScaleSpeed = 10f;
	  
	void Start(){
		gameObject.GetComponent<Camera>().orthographicSize = camSize;
	}

	void FixedUpdate () {
		Vector2 camOffset = new Vector2(0, camOffsetY);
		Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)target.transform.position + camOffset, camSpeed * Time.fixedDeltaTime);
		transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
    }
	  
	public void playerGrowCameraWide(float camGrowAmt, float playerSize){
		camSizeLast = camSize;
		newCamSize = camSize + camGrowAmt;
		camOffsetY = playerSize/2; 
		StartCoroutine(ResizeTime(camSizeLast, newCamSize, 1f));
		//Debug.Log("camSize is: " + camSize);	
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