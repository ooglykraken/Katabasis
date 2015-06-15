using UnityEngine;
using System.Collections;

public class MovableBlock : MonoBehaviour {
	
	private float gravity = 35f;
	
	public Vector3 startPosition;
	public Vector3 verticalOffset = new Vector3(0f, 0f, 3f);
	
	public bool grounded;
	
	private static float timing = Time.deltaTime * 1f;
	
	public void Awake(){
		// startPosition = transform.position;
	}
	
	public void FixedUpdate(){
		if(transform.position.z >= 30f){
			ReturnToPlayer();
		}
		
		grounded = CheckFloor();
		
		GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 6f);
		if(grounded){
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().velocity + new Vector3(0f, 0f, .2f), timing);
		} else {
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().velocity + new Vector3(0f, 0f, gravity), timing);
		}
	}
	
	public bool CheckFloor(){
		RaycastHit hit;
		
		// float distance = 1.1f;
		
		Vector3 ray = transform.position;
		if (Physics.Raycast(ray, Vector3.forward, out hit)){
			if(hit.transform.tag == "Floor"){
				if(hit.distance <=(transform.Find("Collider").gameObject.GetComponent<BoxCollider>().size.z / 2f) + .1f){
					return true;
				}
			}
		}
		
		return false;
	}
	
	public void ReturnToPlayer(){
		transform.position = startPosition - verticalOffset;
	}
}
