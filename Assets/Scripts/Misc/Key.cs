using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public void FixedUpdate(){
		GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 6f);
	}
	
	public void PickUpKey(){
		transform.parent.gameObject.GetComponent<AudioSource>().Play();
		
		// This script is attached to the key's collider
		gameObject.GetComponent<Collider>().enabled = false;
		transform.parent.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
}
