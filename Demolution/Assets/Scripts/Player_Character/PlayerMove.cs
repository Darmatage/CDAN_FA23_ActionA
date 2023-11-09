using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //public Animator animator;
    public Rigidbody2D rb2D;
    private bool FaceRight = true; // determine which way the player is faceing
    public static float runSpeed = 10f;
    public float startSpeed = 10f;
    public bool isAlive = true;
    //public AudioSource WalkSFX
    private Vector3 hMove;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if (canMove){
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if (isAlive == true){
          transform.position = transform.position + hMove * runSpeed * Time.deltaTime;

          // turning: reverse if input is moving the player right and the player faces left
          if((hMove.x <0 && !FaceRight) || (hMove.x >0 && FaceRight)){
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
      transform.localScale = theScale;
    }
}
