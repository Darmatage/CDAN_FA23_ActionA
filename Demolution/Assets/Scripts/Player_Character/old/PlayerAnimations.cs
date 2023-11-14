using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

private Rigidbody2D rb2d;
public Animator anim;
private bool isAlive = true;
public float speed = 3f;
private Vector3 change;

    // Start is called before the first frame update
    void UpdateAnimationAndMove() {
                 if (isAlive == true){
                        if (change!=Vector3.zero) {
                               rb2d.MovePosition(transform.position + change * speed * Time.deltaTime);
                               anim.SetBool("Walk", true);
                               //if (!audioWalk.isPlaying){ audioWalk.Play(); }
                        } else {
                               anim.SetBool("Walk", false);
                               //audioWalk.Stop();
                             }
                  }
    }
}
