using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	
	public AudioSource boxDrag;
	public AudioSource boxDrop;
	
	private float gravity = 8f;
	
	public Vector3 startPosition;
	public Vector3 verticalOffset = new Vector3(0f, 0f, 3f);
	
	public Vector3 startScale;
	
	private Vector3 lastPosition;
	
	private int droppingDistance = 10;
	
	private float seaLevel = -1f;
	
	public bool grounded;
	// public bool isDoomed;
	
	private static float timing = 2f;
	
	// public float lifespan;
	
	private int dragCooldown;
	private int dragTiming = 120;
	
	public void Awake(){
		lastPosition = transform.position;
		startPosition = transform.position;
		grounded = false;
		
		
	}
	
	public void Update(){
		
		
		grounded = CheckFloor();
	}
	
	public void FixedUpdate(){

		
		if(grounded){
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 2 * timing);
		} else {
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().velocity + new Vector3(0f, 0f, gravity), Time.deltaTime * timing);
			
			if(transform.position.z > seaLevel){
				float sizeRatio = transform.position.z / droppingDistance;
				// Debug.Log(sizeRatio);
				transform.localScale = startScale * (1 - sizeRatio);
			}
		}
	}
	
	public bool CheckFloor(){
		RaycastHit hit;
		
		// float distance = 1.1f;
		
		Vector3 ray = transform.position;
		if (Physics.Raycast(ray, Vector3.forward, out hit)){
			if(hit.transform.parent != null && (hit.transform.parent.tag == "Floor" || hit.transform.tag == "ConveyorBelt" || hit.transform.parent.tag == "FloorSwitch")){
				if(hit.distance <= (transform.Find("Collider").transform.localScale.z / 2f) + .2f){
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
	
	public void Resize(){
		transform.localScale = startScale;
	}
}
