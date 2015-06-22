using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	private GameObject openDoor;
	
	private SpriteRenderer sprite;
	
	private new Collider collider;
	
	public void Awake(){
		sprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		collider = transform.Find("Collider").gameObject.GetComponent<Collider>();
	}
	
	public void Open(){
		GetComponent<AudioSource>().Play();
		
		sprite.enabled = false;
		collider.enabled = false;
		
		
	}
	
	public void Close(){
		GetComponent<AudioSource>().Play();
		
		collider.enabled = true;
		sprite.enabled = true;
	}
}
