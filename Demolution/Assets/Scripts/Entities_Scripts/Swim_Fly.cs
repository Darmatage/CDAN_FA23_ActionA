using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_SwimFly : MonoBehaviour{
	
	public GameObject creature;
	public GameObject creatureArt;
	public Transform StartPoint;
	public Transform EndPoint;
	
	public float spawnDelay = 1f;
	public float creatureSpeed = 0.05f;
	public bool faceRight = true;
	private bool canMove = true;
	
    // Start is called before the first frame update
    void Start(){
        creatureArt.SetActive(true);
		creature.transform.position = StartPoint.position;
		if (!faceRight){
			creature.transform.localScale = new Vector2((creature.transform.localScale.x * -1),creature.transform.localScale.y);
		}
    }

    // Update is called once per frame
    void FixedUpdate(){
		float distToEnd = Vector2.Distance(creature.transform.position, EndPoint.position);
		if ((distToEnd >= 0.5f)&&(canMove==true)){
			LerpAcrossInterval(8f);
			//Vector2 pos = Vector2.Lerp ((Vector2)StartPoint.position, (Vector2)EndPoint.position, creatureSpeed * Time.time);
			//creature.transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
		} else {
			creatureArt.SetActive(false);
			canMove = false;
			StartCoroutine(delayRespawn());
		}
    }
	
	IEnumerator delayRespawn(){
		yield return new WaitForSeconds(spawnDelay);
		creature.transform.position = StartPoint.position;
		creatureArt.SetActive(true);
		canMove = true;
	}
	
	IEnumerator LerpAcrossInterval(float interval){
        float fraction = 0.0f;
        float distance = 0.0f;
        do
        {
            fraction = distance / interval;
            distance += Time.deltaTime;
 
            // Apply Lerp here using 'fraction' as controller alpha, as
            // fraction goes from 0.0 to 1.0 over time "interval"
 
            // for example:
            Vector2 pos = Vector2.Lerp( (Vector2)StartPoint.position, (Vector2)EndPoint.position, fraction);
			creature.transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
 
            // or alternately:
            //float size = Mathf.Lerp( minSize, maxSize, fraction);
 
            // or for color:
            //Color color = Color.Lerp( Color.red, Color.green, fraction);
 
            yield return null;
        } while( fraction < 1.0f);
    }
	
}
