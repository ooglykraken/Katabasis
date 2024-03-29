﻿using UnityEngine;
using System.Collections;

public class BargainingItem : MonoBehaviour {
	
	private Transform playerTransform;
	
	private float gravity = 30f;
	private float detectionDistance = 2f;
	public bool isFollowing;
	private bool grounded;
	
	private Rigidbody thisRigidbody;
	
	public void Awake(){
		isFollowing = false;
		
		playerTransform = Player.Instance().transform;
		
		thisRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	public void Update(){
		if(isFollowing && gameObject.GetComponent<Collider>().enabled){
			gameObject.GetComponent<Collider>().enabled = false;
		} else if(gameObject.name == "LoveLetter" && !isFollowing && PlayerDistance() <= detectionDistance){
			Player.Instance().hasLoveLetter = true;
			FollowPlayer();
		}
	}
	
	public void FixedUpdate(){
		if(isFollowing && playerTransform != null){
			Follow();
		}
	
		// grounded = Ground();
	
		// Gravity();
		
		// if(grounded){
			// Inertia();
		// }
	}
	
	private void Gravity(){
		thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, new Vector3(0f, 0f, gravity), Time.deltaTime);
	}
	
	private void Inertia(){
		thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, Vector3.zero, Time.deltaTime * 2f);
	}
	
	private bool Ground(){
		RaycastHit hit;
		
		Vector3 ray = transform.position;
		
		if(Physics.Raycast(ray, -transform.forward, out hit, 1.5f)){
			if(hit.transform.tag =="Floor"){
				return true;
			}
		}
		
		return false;
	}
	
	public void FollowPlayer(){
		isFollowing = true;
	}
	
	private float PlayerDistance(){
		return Vector3.Distance(transform.position, Player.Instance().transform.position);
	}
	
	private void Follow(){
		Vector3 positionModifier = Vector3.zero;
		switch(Player.Instance().playerDirection){
			case(0):
				positionModifier = -transform.up;
				break;
			case(1):
				positionModifier = -transform.right;
				break;
			case(2):
				positionModifier = transform.up;
				break;
			case(3):
				positionModifier = transform.right;
				break;
			default:
				break;
		}
	
		transform.position = Vector3.Lerp(transform.position, playerTransform.position - positionModifier * 1f, Time.deltaTime * 2f);
	}
}
