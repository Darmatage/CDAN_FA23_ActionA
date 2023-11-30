using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

	private Animator anim;
	private Rigidbody2D rb;
	public float jumpForce = 5f;
	public Transform feet;
	public LayerMask groundLayer;
	public LayerMask enemyLayer;
	public bool canJump = false;
	public int jumpTimes = 0;
	public bool isAlive = true;
    
	//public AudioSource JumpSFX;

	public bool heyIsGrounded = true;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      heyIsGrounded = IsGrounded();

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
    }

    public void Jump() {
		jumpTimes += 1;
		rb.velocity = Vector2.up * jumpForce;
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

	public void jumpForceGrow(){
		jumpForce = jumpForce * 1.02f;
	}

    // DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(feet.position, 0.2f);
	}

}
