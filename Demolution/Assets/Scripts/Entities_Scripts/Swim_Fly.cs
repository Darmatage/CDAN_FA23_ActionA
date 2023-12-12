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
			creature.transform.position = Vector2.MoveTowards(creature.transform.position, EndPoint.position, creatureSpeed * Time.deltaTime);
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
}
