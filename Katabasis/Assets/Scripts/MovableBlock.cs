using UnityEngine;
using System.Collections;

public class MovableBlock : MonoBehaviour {
	
	public float gravity;
	
	public Vector3 startPosition;
	public Vector3 verticalOffset = new Vector3(0f, 0f, 3f);
	
	public bool grounded;
	
	public void Awake(){
		// startPosition = transform.position;
	}
	
	public void FixedUpdate(){
		if(transform.position.z >= 30f){
			ReturnToPlayer();
		}
		
		CheckFloor();
		
		GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 6f);
		if(grounded){
			GetComponent<Rigidbody>().velocity += new Vector3(0f, 0f, .2f);
		} else {
			GetComponent<Rigidbody>().velocity += new Vector3(0f, 0f, gravity);
		}
	}
	
	public void CheckFloor(){
		RaycastHit hit;
		
		// float distance = 1.1f;
		
		Vector3 ray = transform.position;
		if (Physics.Raycast(ray, Vector3.forward, out hit)){
			if(hit.transform.tag == "Floor"){
				if(hit.distance >= .1f){
					grounded = true;
				}
			}
		}
	}
	
	public void ReturnToPlayer(){
		transform.position = startPosition - verticalOffset;
	}
}
