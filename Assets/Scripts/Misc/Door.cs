using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	private GameObject openDoor;
	
	private Renderer model;
	
	private new Collider collider;
	
	public bool open;
	
	public void Awake(){
		model = transform.Find("Model").gameObject.GetComponent<Renderer>();
		collider = transform.Find("Collider").gameObject.GetComponent<Collider>();
		
		open = !model.enabled;
	}
	
	public void Open(){
		GetComponent<AudioSource>().Play();
		
		model.enabled = false;
		collider.enabled = false;
		
		open = true;
	}
	
	public void Close(){
		GetComponent<AudioSource>().Play();
		
		collider.enabled = true;
		model.enabled = true;
		
		open = false;
	}
}
