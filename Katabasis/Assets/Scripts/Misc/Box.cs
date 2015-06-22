using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	
	public AudioSource boxDrag;
	public AudioSource boxDrop;
	
	private float gravity = 8f;
	
	public Vector3 startPosition;
	public Vector3 verticalOffset = new Vector3(0f, 0f, 3f);
	
	private Vector3 lastPosition;
	
	public bool grounded;
	
	private static float timing = 2f;
	
	public void Awake(){
		lastPosition = transform.position;
		// startPosition = transform.position;
		grounded = false;
	}
	
	public void Update(){
		if(BeingMoved()){
			// boxDrag.Play();
		}
		lastPosition = transform.position;
		
		grounded = CheckFloor();
	}
	
	public void FixedUpdate(){
		// if(transform.position.z >= 30f){
			// ReturnToPlayer();
		// }
		
		
		if(grounded){
			// GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().velocity + new Vector3(0f, 0f, .15f), Time.deltaTime * timing);
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 2 * timing);
		} else {
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().velocity + new Vector3(0f, 0f, gravity), Time.deltaTime * timing);
		}
	}
	
	public bool CheckFloor(){
		RaycastHit hit;
		
		// float distance = 1.1f;
		
		Vector3 ray = transform.position;
		if (Physics.Raycast(ray, Vector3.forward, out hit)){
			// Debug.Log(hit.transform.tag + " and I am grounded: " + grounded);
			if(hit.transform.parent.tag == "Floor" || hit.transform.tag == "ConveyorBelt" || hit.transform.parent.tag == "FloorSwitch"){
				if(hit.distance <= (transform.Find("Collider").transform.localScale.z / 2f) + .15f){
					return true;
				}
			}
		}
		
		return false;
	}
	
	private bool BeingMoved(){
		if(transform.position.x != lastPosition.x || transform.position.y != lastPosition.y){
			return true;
		}
		
		return false;
	}
	
	// public void ReturnToPlayer(){
		// transform.position = startPosition - verticalOffset;
	// }
}
