using UnityEngine;
using System.Collections;

public class InvisibleFloor : MonoBehaviour {
	public float alphaCap;
	public float alphaChange;
	public float alpha;
	
	private bool alphaIsIncreasing = true;
	
	public void Awake(){
	}
	
	public void FixedUpdate () {
		float newAlpha = renderer.material.color.a;
		
		if(alphaIsIncreasing){
			newAlpha += alphaChange;
		} else {
			newAlpha -= alphaChange;
		}
		
		if(newAlpha >= alphaCap){
			alphaIsIncreasing = false;
		} else if(newAlpha <= 0f){
			alphaIsIncreasing = true;
		}
		
		renderer.material.color = new Vector4(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);
		
		alpha = newAlpha;
	}
}
