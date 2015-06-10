using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour {
	
	private float fadeAdjust = .008f;
	
	public bool fadingIn;
	public bool fadingOut;
	
	private MeshRenderer mesh;
	
	public void Awake(){
		mesh = GetComponent<MeshRenderer>();
		FadeOut();
		Debug.Log("Fading.....");
	}
	
	public void Update(){
		if(fadingOut){
			if(mesh.material.color.a > 0f){
				Debug.Log("fading out");
				mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, mesh.material.color.a - fadeAdjust);
			}
		} else if(fadingIn){
			if(mesh.material.color.a < 1.0f){
				Debug.Log("fading in");
				mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, mesh.material.color.a + fadeAdjust);
			}
		} else {
			
		}
	}
	
	public void FadeIn(){
		fadingOut = false;
		fadingIn = true;
		
	}
	
	public void FadeOut(){
		fadingIn = false;
		fadingOut = true;
		
	}
	
	private static FadeToBlack instance;
	
	public static FadeToBlack Instance(){
		if(instance == null){
			instance = GameObject.Find("FadeToBlack").GetComponent<FadeToBlack>();
		}
		
		return instance;
	}
}
