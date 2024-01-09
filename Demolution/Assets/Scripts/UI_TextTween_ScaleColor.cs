using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class UI_TextTween_ScaleColor : MonoBehaviour{
	//NOTE1: Add this script to health Text, then activate when the player gets a health boost.
	//NOTE2: set Inspector curves to a hump: rightcick middle to add keyframe, then set values 1, 1.3, 1
	public AnimationCurve curveScale = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	public AnimationCurve curveColor = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	float elapsed = 0f;
	Vector3 startScale;
	//public Image thisImage; //to animate the text background frame also 
	//private TextMeshProUGUI UI_Text;
	private Text UI_Text; // legacy Text version

	private Color startColor;
	public Color animColor; // In Inspector set color and set Alpha = visible; 
	private float startR, startG, startB, startA, animR, animG, animB, animA, newR, newG, newB, newA;

	public bool tweenEnabled = false;

	void Start(){
		startScale = transform.localScale;
		//thisImage = GetComponent<Image>();
		//thisImage.color = new Color(2.55f, 2.55f, 2.55f, 0f);
		//UI_Text = GetComponentInChildren<TextMeshProUGUI>();
		UI_Text = GetComponentInChildren<Text>(); // legacy Text version
		startColor = UI_Text.color;
		startR = startColor.r;
		startG = startColor.g;
		startB = startColor.b;
		startA = startColor.a;
		animR = animColor.r;
		animG = animColor.g;
		animB = animColor.b;
		animA = 1f;
	}

	void Update(){
		//temp tester for effect
		if (Input.GetKeyDown("m")){
			EnableTweens();
		}
	}

	void FixedUpdate () {
		if (tweenEnabled==true){
			elapsed += Time.deltaTime;
			
			// Tween Move:
			transform.localScale = startScale * curveScale.Evaluate(elapsed);
			//thisImage.transform.localScale = startScale * curveScale.Evaluate(elapsed);

			// Tween Alpha:
			if (elapsed <= 1f){
				float newColValue = curveColor.Evaluate(elapsed);
				
				if (animR > startR){newR = startR +((animR-startR)*newColValue);}
				else{newR = startR -((startR-animR)*newColValue);}
				
				if (animG > startG){newG = startG +((animG-startG)*newColValue);}
				else{newG = startG -((startG-animG)*newColValue);}
				
				if (animB > startB){newB = startB +((animB-startB)*newColValue);}
				else {newB = startB -((startB-animB)*newColValue);}
				
				if (animA > startA){newA = startA +((animA-startA)*newColValue);}
				else {newA = startA -((startA-animA)*newColValue * -1);}
				
				//thisImage.color = new Color(2.55f, 2.55f, 2.55f, newAlpha);
				UI_Text.color = new Color(newR, newG, newB, newA);
				
				//Debug.Log("current color: " + UI_Text.color + ", start color: " + startColor + ", change: " + newR + ", " + newG + ", " + newB + ", " + newA);
			} 
			
			//end the tween and return to starting values
			else {
				tweenEnabled=false;
				transform.localScale = startScale;
				UI_Text.color = startColor;
				elapsed =0;
			}
		}
	}
	
	public void EnableTweens(){
		tweenEnabled = true;	
	}
	
}