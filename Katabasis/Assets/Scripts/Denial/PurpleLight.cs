using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleLight : MonoBehaviour {
	
	public List<GameObject> revealedObjects = new List<GameObject>();
	
	public void Update(){
		
	}
	
	public void OnTriggerEnter(Collider c){
		if(c.tag == "PurpleLight"){
			// Debug.Log(c.transform.parent.gameObject.name);
			switch(c.transform.parent.gameObject.name){
				case ("Box-Fake"):
					revealedObjects.Add(c.transform.parent.gameObject);
					FakeBoxOff(c.transform.parent.gameObject);
					break;
				case ("Box-Invisible"):
					revealedObjects.Add(c.transform.parent.gameObject);
					InvisibleBoxOn(c.transform.parent.gameObject);
					break;
				default:
					break;
			}
		} else {
			// Debug.Log(c.transform.gameObject.name);
			switch(c.gameObject.name){
				case ("PurpleLightFloor"):
					revealedObjects.Add(c.gameObject);
					ChangeFloor(c.gameObject);
					break;
				case ("PurpleLightWall"):
					revealedObjects.Add(c.gameObject);
					ChangeWall(c.gameObject);
					break;
				case ("PurpleLightDoor"):
					revealedObjects.Add(c.gameObject);
					ChangeDoor(c.gameObject);
					break;
				default:
					break;
			}
		}
	}
	
	public void OnTriggerExit(Collider c){
		if(c.tag == "PurpleLight"){
			// Debug.Log(c.transform.parent.gameObject.name);
			switch(c.transform.parent.gameObject.name){
				case ("Box-Fake"):
					revealedObjects.Remove(c.transform.parent.gameObject);
					FakeBoxOn(c.transform.parent.gameObject);
					break;
				case ("Box-Invisible"):
					revealedObjects.Remove(c.transform.parent.gameObject);
					InvisibleBoxOff(c.transform.parent.gameObject);
					break;
				default:
					break;
			}
		} else {
			// Debug.Log(c.transform.gameObject.name);
			switch(c.gameObject.name){
				case ("PurpleLightFloor"):
					revealedObjects.Remove(c.gameObject);
					ChangeFloor(c.gameObject);
					break;
				case ("PurpleLightWall"):
					revealedObjects.Remove(c.gameObject);
					ChangeWall(c.gameObject);
					break;
				case ("PurpleLightDoor"):
					revealedObjects.Remove(c.gameObject);
					ChangeDoor(c.gameObject);
					break;
				default:
					break;
			}
		}
	}
	
	private void ChangeFloor(GameObject g){
		Renderer floorRenderer = g.GetComponent<Renderer>();
		
		// Debug.Log(g.transform.parent.name);
		
		if(floorRenderer.enabled){
			g.GetComponent<BoxCollider>().center = Vector3.back; 
			floorRenderer.enabled = false;
		} else {
			g.GetComponent<BoxCollider>().center = Vector3.zero;
			floorRenderer.enabled = true;
		}
	}
	
	private void ChangeWall(GameObject g){
		Renderer wallRenderer = g.transform.Find("Model").GetComponent<Renderer>();
		
		if(wallRenderer.enabled){
			g.GetComponent<BoxCollider>().center = 2 * Vector3.forward;
			wallRenderer.enabled = false;
		} else {
			g.GetComponent<BoxCollider>().center = Vector3.zero;
			wallRenderer.enabled = true;
		}
	}
	
	private void ChangeDoor(GameObject g){
		Renderer doorRenderer = g.transform.GetComponent<MeshRenderer>();
		
		if(doorRenderer.enabled)
		{
			g.GetComponent<BoxCollider>().center = 2*Vector3.forward;
			doorRenderer.enabled = false;
		}
		else
		{
			g.GetComponent<BoxCollider>().center = Vector3.zero;
			doorRenderer.enabled = true;
		}
	}
	
	// This means the box is now revealed
	private void FakeBoxOn(GameObject g){
		Transform colliderTransform = g.transform.Find("Collider");
		GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm a fake box and I'm being turned on");
		
		colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
		g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		spriteObject.SetActive(true);

	}
	
	// This means the box is now revealed
	private void InvisibleBoxOn(GameObject g){
		Transform colliderTransform = g.transform.Find("Collider");
		GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm an invisible box and I'm being turned on");
		
		colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, 0f);
		g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		spriteObject.SetActive(true);

	}
	
	private void FakeBoxOff(GameObject g){
		Transform colliderTransform = g.transform.Find("Collider");
		GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm a fake box and I'm being turned off");
		
		colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
		g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		spriteObject.SetActive(false);
	}
	
	private void InvisibleBoxOff(GameObject g){
		Transform colliderTransform = g.transform.Find("Collider");
		GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		
		// Debug.Log("I'm an invisible box and I'm being turned off");
		
		colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
		g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
		spriteObject.SetActive(false);
	}
	
	
	public void LensOff(){
		foreach(GameObject g in revealedObjects){
			if(g.tag == "Box"){
				switch(g.name){
					case ("Box-Fake"):
						FakeBoxOn(g);
						break;
					case ("Box-Invisible"):
						InvisibleBoxOff(g);
						break;
					default:
						Debug.Log("Something is wrong! Lens Off");
						break;
				}
			} else {
				switch(g.name){
					case ("PurpleLightFloor"):
						ChangeFloor(g);
						break;
					case ("PurpleLightWall"):
						ChangeWall(g);
						break;
					case ("PurpleLightDoor"):
						ChangeDoor(g);
						break;
					default:
						break;
				}
			}
		}
		
		revealedObjects.Clear();
	}
	
	private static PurpleLight instance = null;
	
	public static PurpleLight Instance(){
		if(instance == null){
			instance = GameObject.Find("Lens").GetComponent<PurpleLight>();
		}
		
		return instance;
	}
}
