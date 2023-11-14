using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour{

    private Animator anim;
    private Rigidbody2D rb2D;
    public bool FaceRight = true; // determine which way the player is faceing
    public static float runSpeed = 10f;
    public float walkSpeed = 3f;
	//public float speed = 3f;
    public bool isAlive = true;
    //public AudioSource WalkSFX
    private Vector3 hMove;

    public bool canMove = true;

    void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

	void Update(){
		if (canMove){
			hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
			if (isAlive == true){
				transform.position = transform.position + hMove * walkSpeed * Time.deltaTime;

				if (Input.GetAxis("Horizontal") != 0){
					anim.SetBool ("Walk", true);
					//if (!WalkSFX.isPlaying){
					//	WalkSFX.Play();
					//}
				} else {
					anim.SetBool ("Walk", false);
					//WalkSFX.Stop();
				} 


				// turning: reverse if input is moving the player right and the player faces left
				if((hMove.x >0 && !FaceRight) || (hMove.x <0 && FaceRight)){
					playerTurn();
				}
			}
		}
	}
	
    void FixedUpdate(){
      //slow down on hills / stops sliding from velocity
      if(hMove.x == 0){
        rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y);
      }
    }

    private void playerTurn(){
      //Switch player facing label
      FaceRight = !FaceRight;

      //Multiply player's local scale by -1.
      Vector3 theScale = transform.localScale;
	  theScale.x *= -1;
      transform.localScale = theScale;
    }
	
	public void walkSpeedGrow(){
		walkSpeed = walkSpeed * 1.03f;
	}
	
}
