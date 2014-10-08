using UnityEngine;
using System.Collections;

public class MovableBlock : MonoBehaviour {
	
	public Vector3 startPosition;
	public Vector3 verticalOffset = new Vector3(0f, 0f, 3f);
	
	public void Awake(){
		// startPosition = transform.position;
	}
	
	public void FixedUpdate(){
		if(transform.position.z >= 30f){
			ReturnToPlayer();
		}
	
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * 6f);
		rigidbody.velocity += new Vector3(0f, 0f, .8f);
	}
	
	public void ReturnToPlayer(){
		transform.position = startPosition - verticalOffset;
	}
}
