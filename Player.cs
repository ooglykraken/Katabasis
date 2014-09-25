using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float movementSpeed = 5f;
	
	private int verticalDirection;
	private int horizontalDirection;
	
	private bool hasFloorKey;
	
	public void Awake(){
		hasFloorKey = false;
	}
	
	public void OnCollisionEnter(Collision c){
	
		if(c.transform.parent.tag == "Key"){
			PickUpKey(c.transform.parent.gameObject);
		} else if(c.transform.parent.tag == "StairsDown"){
			Descend();
		} else if(c.transform.parent.tag == "Enemy"){
			Death();
		} else if(c.transform.parent.tag == "StairsUp"){
			Finish();
		}
	}
	
	public void Update(){
		verticalDirection = (int)Input.GetAxisRaw("Vertical");
		horizontalDirection = (int)Input.GetAxisRaw("Horizontal");
		
		Move();
	}
	
	public void LateUpdate(){
		CameraFollow();
	}
	
	private void Move(){
		rigidbody.velocity = new Vector3(horizontalDirection * movementSpeed, verticalDirection * movementSpeed, 0f);
		
		if(verticalDirection < 0){
			Turn("south");
		} else if(verticalDirection > 0){
			Turn("north");
		}
		if(horizontalDirection < 0){
			Turn("west");
		} else if(horizontalDirection > 0){
			Turn("east");
		}
	}
	
	private void PickUpKey(GameObject key){
			
		hasFloorKey = true;
		Destroy(key);
		
		// GameObject dialogBox = Instantiate(Resources.Load("DialogBox", typeof(GameObject)) as GameObject) as GameObject;
		// dialogBox.transform.parent = transform;
		// dialogBox.transform.position = Vector3.zero;
	}
	
	private void Descend(){
		if(Gameplay.Instance().currentLevel <= Gameplay.Instance().finalLevel){
			Gameplay.Instance().NextLevel();
		} else {
			//Finish();
		}
	}
	
	private void Death(){
		
	}
	
	private void Finish(){
		
	}
	
	private void Turn(string direction){
		Vector3 newFacing = new Vector3(0f, 0f, 0f);
	
		switch(direction){
			case "north":
				newFacing = new Vector3(0f, 0f, 0f);
				break;
			case "south":
				newFacing = new Vector3(0f, 0f, 180f);	
				break;
			case "east":
				newFacing = new Vector3(0f, 0f, 270f);
				break;
			case "west":
				newFacing = new Vector3(0f, 0f, 90f);
				break;
			default:
				break;
		}
		
		transform.eulerAngles = newFacing;
	}
	
	private void CameraFollow(){
		float cameraOffset = .6f;
		
		Camera main = Camera.main;
		
		if(transform.position.x  >  main.transform.position.x + cameraOffset){
			main.transform.position = new Vector3( transform.position.x - cameraOffset, main.transform.position.y, main.transform.position.z);
		} else if(transform.position.x  <  main.transform.position.x - cameraOffset){
			main.transform.position = new Vector3( transform.position.x + cameraOffset, main.transform.position.y, main.transform.position.z);
		}
		if(transform.position.y  >  main.transform.position.y + cameraOffset){
			main.transform.position = new Vector3(main.transform.position.x, transform.position.y - cameraOffset, main.transform.position.z);
		} else if(transform.position.y  <  main.transform.position.y - cameraOffset){
			main.transform.position = new Vector3(main.transform.position.x, transform.position.y + cameraOffset, main.transform.position.z);
		}
	}
	
	// private static Player instance = null;
	
	// public static Player Instance(){
		// if (instance == null)
			// instance = GameObject.FindObjectOfType<Player>();
			
		// return instance;
	// }
}
