using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

	private Animator anim;
	private Rigidbody2D rb2D;
	public float jumpForce = 5f;
	public Transform feet;
	public LayerMask groundLayer;
	public LayerMask enemyLayer;
	public LayerMask climbLayer;
	public bool canJump = false;
	public int jumpTimes = 0;
	public bool isAlive = true;
    
	//public AudioSource JumpSFX;

	public bool heyIsGrounded = true;
	public bool heyIsClimbable = false;
	public float climbSpeed = 4f;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
      heyIsGrounded = IsGrounded();
	  heyIsClimbable = isClimbable();

        if((IsGrounded()) && (jumpTimes == 0)){
            canJump = true;
            //gameObject.GetComponent<PlayerMove>().canMove = true;
        }
        else if( (jumpTimes <= 1) && (GameHandler.doubleJumpUnlocked == true))
        {
            canJump = true;
        }

        else {
          canJump = false;
          //gameObject.GetComponent<PlayerMove>().canMove = false;
        }

        if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true)) {
            Jump();
        }
		
		//climbable is essentially movement from a top-down game, with vertical enabled:
		if (isClimbable()){
			rb2D.gravityScale = 0;
			Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
			transform.position = transform.position + hvMove * climbSpeed * Time.deltaTime;
			if (Input.GetAxis("Vertical") != 0){
			//anim.SetBool ("Climb", true);
			//     if (!WalkSFX.isPlaying){
			//           WalkSFX.Play();
			//     }
			} else {
			//     anim.SetBool ("Climb", false);
			//     WalkSFX.Stop();
			}
		}
		
    }

    public void Jump() {
		jumpTimes += 1;
		rb2D.velocity = Vector2.up * jumpForce;
		anim.SetTrigger("jump");
		// JumpSFX.Play();
    }

    public bool IsGrounded() {
      Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.2f, groundLayer);
      Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 0.2f, enemyLayer);
      if ((groundCheck != null) || (enemyCheck != null)) {
        jumpTimes = 0;
        return true;
      }
      return false;
    }


	//need to make the climbable radius grow based on player size
	public bool isClimbable(){
		Collider2D climbCheck = Physics2D.OverlapCircle(feet.position, 1f, climbLayer);
		if (climbCheck != null) {
			return true;
		}
		//anim.SetBool("PlayerClimb", false);
		rb2D.gravityScale = 1;
		return false;
	}


	public void jumpForceGrow(){
		jumpForce = jumpForce * 1.02f;
	}

    // DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(feet.position, 0.2f);
	}

}
