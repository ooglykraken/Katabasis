using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float movementSpeed = 5f;
	public float lightLostPerFrame = .001f;
	private float startingLightIntensity;
	private float startingLightRange;
	
	private int verticalDirection;
	private int horizontalDirection;
	
	private bool hasFloorKey;
	
	private Transform lanternTransform;
	
	private GameObject lantern;
	
	public void Awake(){
		hasFloorKey = false;
		
		lanternTransform = transform.Find("Lantern");
		lantern = lanternTransform.gameObject;
		
		startingLightRange = lantern.GetComponent<Light>().range;
		startingLightIntensity = lantern.GetComponent<Light>().intensity;
	}
	
	public void OnCollisionEnter(Collision c){
		
		if(c.transform.parent)
		{
			if(c.transform.parent.tag == "Key"){
				PickUpKey(c.transform.parent.gameObject);
			} else if(c.transform.parent.tag == "StairsDown"){
				if(hasFloorKey){
					Descend();
				} else {
					//He doesnt have the key yet
				}
			} else if(c.transform.parent.tag == "Enemy"){
				Death();
			} else if(c.transform.parent.tag == "StairsUp"){
				Finish();
			} else {
			}
		}
	}
	
	public void Update(){
		verticalDirection = (int)Input.GetAxisRaw("Vertical");
		horizontalDirection = (int)Input.GetAxisRaw("Horizontal");
		
		Move();
		
		LoseLight();
		
		if(!CheckForWalls()){
			lantern.GetComponent<Light>().range = startingLightRange;
		}
	}
	
	public void LateUpdate(){
		CameraFollow();
	}
	
	private void Move(){
		rigidbody.velocity = new Vector3(horizontalDirection * movementSpeed, verticalDirection * movementSpeed, 0f);
		
		if(verticalDirection < 0 && horizontalDirection == 0){
			Turn("down");
		} else if(verticalDirection > 0 && horizontalDirection == 0){
			Turn("up");
		} else if(horizontalDirection < 0 && verticalDirection == 0){
			Turn("left");
		} else if(horizontalDirection > 0 && verticalDirection == 0){
			Turn("right");
		} else if(horizontalDirection > 0 && verticalDirection < 0){
			Turn("r-d");
		} else if(horizontalDirection < 0 && verticalDirection < 0){
			Turn("l-d");
		} else if(horizontalDirection > 0 && verticalDirection > 0){
			Turn("r-u");
		} else if(horizontalDirection < 0 && verticalDirection > 0){
			Turn("l-u");
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
			lantern.GetComponent<Light>().intensity = startingLightIntensity;
		} else {
			//Finish();
		}
		
		hasFloorKey = false;
	}
	
	private void LoseLight(){
		
		lantern.GetComponent<Light>().intensity -= lightLostPerFrame;
		lantern.GetComponent<Light>().spotAngle = lantern.GetComponent<Light>().intensity * 10f;
	}
	
	private bool CheckForWalls(){
		
		float distance = startingLightRange;
		RaycastHit hit;
	
		Vector3 ray  = new Vector3(transform.position.x , transform.position.y , 0f);
		if (Physics.Raycast(ray, transform.up, out hit)) {
			if (Vector3.Distance(transform.position, hit.point) <= distance){
				//hitPoint = hit.point;
				//return hit.collider.gameObject;
				
				lantern.GetComponent<Light>().range = Vector3.Distance(transform.position, hit.point);
				return true;
			}
		}
		
		return false;
	}
	
	private void Death(){
		
	}
	
	private void Finish(){
		
	}
	
	private void Turn(string direction){
		Vector3 newFacing = new Vector3(0f, 0f, 0f);
	
		switch(direction){
			case "up":
				newFacing = new Vector3(0f, 0f, 0f);
				break;
			case "down":
				newFacing = new Vector3(0f, 0f, 180f);	
				break;
			case "left":
				newFacing = new Vector3(0f, 0f, 90f);
				break;
			case "right":
				newFacing = new Vector3(0f, 0f, 270f);
				break;
			case "r-d":
				newFacing = new Vector3(0f, 0f, 225f);
				break;
			case "l-d":
				newFacing = new Vector3(0f, 0f, 135f);
				break;
			case "r-u":
				newFacing = new Vector3(0f, 0f, 325f);
				break;
			case "l-u":
				newFacing = new Vector3(0f, 0f, 45f);
				break;
			default:
				break;
		}
		
		transform.eulerAngles = newFacing;
	}
	
	private void CameraFollow(){
		float cameraOffset = .6f;
		float cameraTiltAdjustment = -3;
		Camera main = Camera.main;
		
		if(transform.position.x  >  main.transform.position.x + cameraOffset){
			main.transform.position = new Vector3( transform.position.x - cameraOffset, main.transform.position.y, main.transform.position.z);
		} else if(transform.position.x  <  main.transform.position.x - cameraOffset){
			main.transform.position = new Vector3( transform.position.x + cameraOffset, main.transform.position.y, main.transform.position.z);
		}
		if(transform.position.y  >  main.transform.position.y + cameraOffset){
			main.transform.position = new Vector3(main.transform.position.x, transform.position.y - cameraOffset + cameraTiltAdjustment, main.transform.position.z);
		} else if(transform.position.y  <  main.transform.position.y - cameraOffset){
			main.transform.position = new Vector3(main.transform.position.x, transform.position.y + cameraOffset + cameraTiltAdjustment, main.transform.position.z);
		}
	}
	
	// private static Player instance = null;
	
	// public static Player Instance(){
		// if (instance == null)
			// instance = GameObject.FindObjectOfType<Player>();
			
		// return instance;
	// }
}
