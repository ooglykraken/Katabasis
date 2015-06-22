using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleLight : MonoBehaviour {
	
	public List<GameObject> revealedObjects = new List<GameObject>();
	
	private List<GameObject> allPurpleLightObjects = new List<GameObject>();
	
	private static float distanceToReveal = 4.5f;
	
	public void Awake(){
		allPurpleLightObjects.Clear();
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("PurpleLight")){
			allPurpleLightObjects.Add(g.transform.parent.gameObject);
		}
	}
	
	public void Update(){
		foreach(GameObject g in allPurpleLightObjects){
			if(revealedObjects.Contains(g) && DistanceFromPlayer(g.transform) > distanceToReveal){
				ObjectLeavingZone(g);
			} else if(!revealedObjects.Contains(g) && DistanceFromPlayer(g.transform) <= distanceToReveal){
				ObjectEnteringZone(g);
			} else { 
			}
		}
	}
	
	public void ObjectEnteringZone(GameObject g){
		revealedObjects.Add(g);
		Change(g);
	}
	
	public void ObjectLeavingZone(GameObject g){
		revealedObjects.Remove(g);
		Change(g);
	}
	
	// public void OnTriggerExit(Collider c){
		// if(c.tag == "PurpleLight"){
			// Debug.Log(c.transform.parent.name);
			// switch(c.transform.parent.gameObject.name){
				// case ("Box-Invisible"):
					// revealedObjects.Remove(c.transform.parent.gameObject);
					// Change(c.transform.parent.gameObject);
					// break;
				// case ("PurpleLightFloor"):
					// revealedObjects.Remove(c.transform.parent.gameObject);
					// Change(c.transform.parent.gameObject);
					// break;
				// case ("PurpleLightWall"):
					// revealedObjects.Remove(c.transform.parent.gameObject);
					// Change(c.transform.parent.gameObject);
					// break;
				// case ("PurpleLightDoor"):
					// revealedObjects.Remove(c.transform.parent.gameObject);
					// Change(c.transform.parent.gameObject);
					// break;
				// default:
					// break;
			// }
		// }
	// }
	
	public void LensOff(){
		foreach(GameObject g in revealedObjects){
			Change(g);
		}
		
		revealedObjects.Clear();
	}
	
	private float DistanceFromPlayer(Transform t){
		return Vector3.Distance(t.position, transform.parent.position);
	}
	
	private void Change(GameObject g){

		if(g.transform.Find("Sprite")){
		
			SpriteRenderer renderer = g.transform.Find("Sprite").GetComponent<SpriteRenderer>();
			BoxCollider collider = g.transform.Find("Collider").GetComponent<BoxCollider>();
			
			if(g.name == "Box-Invisible"){
				if(renderer.color.a != 1){
					renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
				}else {
					renderer.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, .2f);
				}
				return;
			}
			
			if(renderer.enabled)
			{
				renderer.enabled = false;
				collider.enabled = false;
			}
			else
			{
				renderer.enabled = true;
				collider.enabled = true;
			}
			
		} else if(g.transform.Find("Model")){
		
			MeshRenderer renderer = g.transform.Find("Model").GetComponent<MeshRenderer>();
			BoxCollider collider = g.transform.Find("Collider").GetComponent<BoxCollider>();
			
			if(renderer.enabled)
			{
				renderer.enabled = false;
				collider.enabled = false;
			}
			else
			{
				renderer.enabled = true;
				collider.enabled = true;
			}
		} else {
			Debug.Log("Purple light object not accounted for");
		}
	}

	private static PurpleLight instance = null;
	
	public static PurpleLight Instance(){
		if(instance == null){
			instance = GameObject.Find("Lens").GetComponent<PurpleLight>();
		}
		
		return instance;
	}
	// private void ChangeFloor(GameObject g){
		// Renderer floorRenderer = g.GetComponent<Renderer>();
		
		// Debug.Log(g.transform.parent.name);
		
		// if(floorRenderer.enabled){
			// g.GetComponent<BoxCollider>().center = Vector3.back; 
			// floorRenderer.enabled = false;
		// } else {
			// g.GetComponent<BoxCollider>().center = Vector3.zero;
			// floorRenderer.enabled = true;
		// }
	// }
	
	// private void ChangeWall(GameObject g){
		// MeshRenderer wallRenderer = g.transform.Find("Model").GetComponent<MeshRenderer>();
		
		// if(wallRenderer.enabled){
			// g.GetComponent<Collider>().enabled = false;
			// wallRenderer.enabled = false;
		// } else {
			// g.GetComponent<BoxCollider>().center = Vector3.zero;
			// g.GetComponent<Collider>().enabled = true;
			// wallRenderer.enabled = true;
		// }
	// }
	
	// private void ChangeDoor(GameObject g){
		// SpriteRenderer doorRenderer = g.transform.Find("Sprite").GetComponent<SpriteRenderer>();
		// BoxCollider doorCollder = g.transform.Find("Collider").GetComponent<BoxCollider>();
		
		// Debug.Log(doorRenderer.enabled);
		
		// if(doorRenderer.enabled)
		// {
			// doorCollder.enabled = false;
			// doorRenderer.enabled = false;
		// }
		// else
		// {
			// doorCollder.enabled = true;
			// doorRenderer.enabled = true;
		// }
	// }
	
	// This means the box is now revealed
	// private void FakeBoxOn(GameObject g){
		// Transform colliderTransform = g.transform.Find("Collider");
		// GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm a fake box and I'm being turned on");
		
		// colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
		// g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		// spriteObject.SetActive(true);

	// }
	
	// This means the box is now revealed
	// private void InvisibleBoxOn(GameObject g){
		// Transform colliderTransform = g.transform.Find("Collider");
		// GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm an invisible box and I'm being turned on");
		
		// colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, 0f);
		// g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		// spriteObject.SetActive(true);

	// }
	
	// private void FakeBoxOff(GameObject g){
		// Transform colliderTransform = g.transform.Find("Collider");
		// GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm a fake box and I'm being turned off");
		
		// colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
		// g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		// spriteObject.SetActive(false);
	// }
	
	// private void InvisibleBoxOff(GameObject g){
		// Transform colliderTransform = g.transform.Find("Collider");
		// GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm an invisible box and I'm being turned off");
		
		// colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
		// g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		// spriteObject.SetActive(false);
	// }
}
