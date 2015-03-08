using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleLight : MonoBehaviour {
	
	private List<GameObject> revealedObjects = new List<GameObject>();
	
	public void Update(){
		
	}
	
	public void OnTriggerEnter(Collider c){
		if(c.tag == "PurpleLight"){
			// Debug.Log(c.transform.parent.gameObject.name);
			switch(c.transform.parent.gameObject.name){
				case ("Box-Fake"):
					revealedObjects.Add(c.transform.parent.gameObject);
					ChangeFakeBox(c.transform.parent.gameObject);
					break;
				case ("Box-Invisible"):
					revealedObjects.Add(c.transform.parent.gameObject);
					ChangeInvisibleBox(c.transform.parent.gameObject);
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
					ChangeFakeBox(c.transform.parent.gameObject);
					break;
				case ("Box-Invisible"):
					revealedObjects.Remove(c.transform.parent.gameObject);
					ChangeInvisibleBox(c.transform.parent.gameObject);
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
	
	private void ChangeFakeBox(GameObject g){
		Transform colliderTransform = g.transform.Find("Collider");
		GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		if(spriteObject.activeSelf){
			// if it's already on
			colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, 0f);
			g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
			spriteObject.SetActive(false);
		} else {
			// if it's off
			colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
			g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
			spriteObject.SetActive(true);
		}
	}
	
	private void ChangeInvisibleBox(GameObject g){
		Transform colliderTransform = g.transform.Find("Collider");
		GameObject spriteObject = g.transform.Find("Sprite").gameObject;
		if(spriteObject.activeSelf){
			// if it's already on
			colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, -2f);
			g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
			spriteObject.SetActive(false);
		} else {
			// if it's off
			colliderTransform.position = new Vector3(colliderTransform.position.x, colliderTransform.position.y, 0f);
			g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0f);
			spriteObject.SetActive(true);
		}
	}
	
	private void ChangeFloor(GameObject g){
		Renderer floorRenderer = g.GetComponent<Renderer>();
		
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
		
	}
}
