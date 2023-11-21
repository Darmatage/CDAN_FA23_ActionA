using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
	public Transform boundaryLeft;
	public Transform boundaryRight;
	private Transform destination;
	private float distToDestination;
	public float speed = 2f;
	private Rigidbody2D rb; 
	private bool faceRight = true;
	private bool canMove = true;
	
    void Start(){
        rb = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate(){
        if (faceRight){destination = boundaryRight;}
		else {destination = boundaryLeft;}
		
		distToDestination = Vector2.Distance(transform.position, destination.position);
		
		if (canMove){
			transform.position = Vector2.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
		}
		
		if (distToDestination < 1f){
			CarTurn();
		}
    }
	
	
	private void CarTurn(){
		// NOTE: Switch player facing label (avoids constant turning)
		faceRight = !faceRight;

		// NOTE: Multiply player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	} 
	
	public void isCrushed(){
		canMove = false;
	}
	
	
	
}
