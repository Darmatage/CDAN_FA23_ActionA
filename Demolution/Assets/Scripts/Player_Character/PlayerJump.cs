using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

  public Rigidbody2D rb;
  public float jumpForce = 20f;
  public Transform feet;
  public LayerMask groundLayer;
  public LayerMask enemyLayer;
  public bool canJump = false;
  public int jumpTimes = 0;
  public bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if((IsGrounded()) && (jumpTimes == 0)){
            canJump = true;
        }
        else if (jumpTimes > 0){
          canJump = false;
        }

        if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true)) {
            Jump();
        }
    }

    public void Jump() {
      jumpTimes += 1;
      rb.velocity = Vector2.up * jumpForce;
    }

    public bool IsGrounded() {
      Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);
      Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 0.5f, enemyLayer);
      if ((groundCheck != null) || (enemyCheck != null)) {
        jumpTimes = 0;
        return true;
      }
      return false;
    }


    // DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(feet.position, 0.5f);
      }

}
