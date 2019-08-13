using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lens : MonoBehaviour {
	
	public List<GameObject> allLensObjects = new List<GameObject>();

	private static float timeToDisappear = 2000f;
	public float timer;
	
	public bool willPing;
	public bool isActivated;
	
	// private Transform parentCollider;
	
	public void Start(){
		// allPurpleLightObjects.Clear();
		// allPurpleLightObjects = GameObject.FindGameObjectsWithTag("PurpleLight");
		// foreach(GameObject g in GameObject.FindGameObjectsWithTag("PurpleLight")){
			// if(!allPurpleLightObjects.Contains(g.transform.parent.gameObject)){
				// allPurpleLightObjects.Add(g.transform.parent.gameObject);
			// }
		// }
		
		// parentCollider = transform.parent.Find("Collider");
		timer = 0f;
		isActivated = false;
		willPing = false;
	}
	
	public void Update(){
		
		if(willPing)
				CheckPing();
		
		if(timer <= 0f && isActivated){
			isActivated = false;
			Switch();
		} else if(timer > 0f){
			
			timer--;
			Fading();
			// Debug.Log("Fading");
		}		
		
		// if(Input.GetKeyUp(KeyCode.LeftArrow)){
			// Ping();
		// }
	}
	
	public void Ping(){
		
		// Debug.Log("Pinging");
		willPing = true;
		transform.Find("Ping").gameObject.GetComponent<Light>().intensity = 2f;
		timer = timeToDisappear;
		if(isActivated){
			return;
		}
		Switch();
		isActivated = true;
		
	}
	
	private void CheckPing(){
		
		Light ping = transform.Find("Ping").gameObject.GetComponent<Light>();
		float coverage = ping.GetComponent<Light>().spotAngle;
		
		if(coverage == 179f){
			ping.spotAngle = 0f;
			ping.intensity = 0f;
			willPing = false;
		} else {
			ping.GetComponent<Light>().spotAngle += 11f;
		}
	}
	
	private void Switch(){
		
		int counter = 0;
	
		foreach(GameObject g in allLensObjects){
			
			// Debug.Log(counter);
			counter++;
			if(g.transform.Find("Sprite")){
				
				// if(g.name == "PurpleLightDoor" && g.GetComponent<Door>().open){
					
				// }
				
				SpriteRenderer renderer = g.transform.Find("Sprite").GetComponent<SpriteRenderer>();
				BoxCollider collider = g.transform.Find("Collider").GetComponent<BoxCollider>();
				
				renderer.enabled = true;
				
				if(g.name == "Box-Invisible"){
					if(isActivated){
						renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
					}else {
						renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, .2f);
					}
					
				} else {
				
					if(collider.enabled)
					{
						renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
						collider.enabled = false;
					}
					else
					{
						renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
						collider.enabled = true;
					}
				}
				
			} else if(g.transform.Find("Model")){
				
				if(g.name == "PurpleLightDoor" && g.GetComponent<Door>().open){
					
				}
				
				MeshRenderer renderer = g.transform.Find("Model").GetComponent<MeshRenderer>();
				BoxCollider collider = g.transform.Find("Collider").GetComponent<BoxCollider>();
				
				renderer.enabled = true;
				
				if(collider.enabled)
				{
					renderer.material.color = new Vector4(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0f);
					collider.enabled = false;
				}
				else
				{
					renderer.material.color = new Vector4(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
					collider.enabled = true;
				}
			} else if(g.transform.Find("FloorText")){
				MeshRenderer renderer = g.transform.Find("FloorText").GetComponent<MeshRenderer>();
				
				if(renderer.enabled){
					renderer.enabled = false;
				} else {
					renderer.enabled = true;
				}
			}else {
				Debug.Log("Purple light object not accounted for");
			}
		}
	}
	
	private void Fading(){
		foreach(GameObject g in allLensObjects){
			if(g.transform.Find("Sprite")){
				// if(g.name == "PurpleLightDoor" && g.GetComponent<Door>().open){
					// return;
				// }
				SpriteRenderer renderer = g.transform.Find("Sprite").GetComponent<SpriteRenderer>();
				BoxCollider collider = g.transform.Find("Collider").GetComponent<BoxCollider>();
				
				if(collider.enabled)
				{
					float newAlpha = (timer/timeToDisappear) + .2f;
					if(newAlpha > 1f){
						newAlpha = 1f;
					}
					renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha);
				}
				else
				{
					renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, .9f - (timer/timeToDisappear));
				}
				
			} else if(g.transform.Find("Model")){
				
				if(g.name == "PurpleLightDoor" && g.GetComponent<Door>().open){
					return;
				}
				
				MeshRenderer renderer = g.transform.Find("Model").GetComponent<MeshRenderer>();
				BoxCollider collider = g.transform.Find("Collider").GetComponent<BoxCollider>();
				
				if(collider.enabled)
				{
					float newAlpha = (timer/timeToDisappear) + .2f;
					if(newAlpha > 1f){
						newAlpha = 1f;
					}
					renderer.material.color = new Vector4(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);
				}
				else
				{
					renderer.material.color = new Vector4(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, .9f - (timer/timeToDisappear));
				}
				
			}else {
				Debug.Log("Purple light object not accounted for");
			}
		}
	}


	private static Lens instance = null;
	
	public static Lens Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<Lens>();
		}
		
		return instance;
	}
}
