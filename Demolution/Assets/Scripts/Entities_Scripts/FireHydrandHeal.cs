using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrandHeal : MonoBehaviour
{
	public bool dead = false;
	private Animator anim;
	public int maxHealth = 80;
	public int currentHealth;
	private GameHandler ghandler;

    // Start is called before the first frame update
    void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		currentHealth = maxHealth;
		dead = false;
    }

    // Update is called once per frame
    public void TakeDamage(int damage){
  		currentHealth -= damage;
  		Debug.Log("I got hit" + gameObject.name);
  		//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
  		//StartCoroutine(ResetColor());
  		//anim.SetTrigger ("Hurt");
  		if (currentHealth <= 0){
  			Die();
  		}
  	}

  	void Die(){
      ghandler.playerGetHealth(15);
      dead = true;
    }
}
