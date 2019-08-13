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
		float newAlpha = GetComponent<Renderer>().material.color.a;
		
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
		
		GetComponent<Renderer>().material.color = new Vector4(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, newAlpha);
		
		alpha = newAlpha;
	}
}
