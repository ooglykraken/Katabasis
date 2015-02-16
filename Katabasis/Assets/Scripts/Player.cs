using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float movementSpeed;
	public float lightLostPerFrame = .001f;
	private float startingLightIntensity;
	private float startingLightRange;
	
	private int verticalDirection;
	private int horizontalDirection;
	
	public bool hasFloorKey;
	public bool isWalking;
	private bool isDoorOpen;
	
	//Added for SmokeEnemy 
	private bool isSlowed;
	
	private Transform lanternTransform;
	
	private GameObject lantern;
	
	public void Awake(){
		hasFloorKey = false;
		
		//Added for SmokeEnemy slow effect
		isSlowed = false;
		
		lanternTransform = transform.Find("Lantern");
		lantern = lanternTransform.gameObject;
		
		startingLightRange = lantern.GetComponent<Light>().range;
		startingLightIntensity = lantern.GetComponent<Light>().intensity;
	}
	
	// Click player to throw the Smoke Enemy off.
	public void OnMouseDown()
	{
		
		foreach ( Transform child in transform)
		{
			 if (child.tag == "SmokeEnemy")
			 {
			 	Debug.Log ("clicked");
			 	child.GetComponent<SmokeEnemy>().thrownOff = true;
			 }
			 else
			 {
			 }
		}
		isSlowed = false;
	}
	
	public void OnCollisionEnter(Collision c){
		
		if(c.transform.parent)
		{
			if(c.transform.parent.tag == "Key"){
				PickUpKey(c.transform.parent.gameObject);
			} else if(c.transform.parent.tag == "StairsDown"){
				// if(isDoorOpen){
					// Descend();
				// }else
				if(hasFloorKey){
					Descend();
					// OpenDoorDown();
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
		
		if (c.transform.tag.Equals ("SmokeEnemy"))
		{
			Debug.Log ("Hit by Smoke");
			isSlowed = true;
		}
	}
	
	public void FixedUpdate(){
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_MAC
			verticalDirection = (int)Input.GetAxisRaw("Vertical");
			horizontalDirection = (int)Input.GetAxisRaw("Horizontal");
		#endif
		
		string floorCast = CheckFloor();
		if(floorCast == "Floor"  || floorCast == "FloorSwitch" || floorCast == "InvisibleFloor"){
			Move();
		} else {
			rigidbody.velocity = Vector3.zero;
		}
		
		// LoseLight();
		
		if(!CheckForWalls() ){
			lantern.GetComponent<Light>().range = startingLightRange;
		}
		
		//Check the SmokeEnemy related stuff
		Slowed();	
		CheckForSmokeEnemy();
		
	}
	
	public void LateUpdate(){
		CameraFollow();
	}
	
	private void Move(){
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
	
		rigidbody.velocity = new Vector3(horizontalDirection * movementSpeed, verticalDirection * movementSpeed, 0f);
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime);
		
		if(horizontalDirection == 0 && verticalDirection == 0){
			gameObject.audio.Stop();
			isWalking = false;
		} else {
			if(!isWalking){
				gameObject.audio.Play();
			}
			isWalking = true;
		}
	}
	
	private void Slowed()
	{
		if (isSlowed == true){
			movementSpeed = 2.5f;
			lightLostPerFrame = .003f;
		}else{
			movementSpeed = 5f;
			isSlowed = false;
			lightLostPerFrame = .001f;
		}
	}
	
	private void PickUpKey(GameObject key){
			
		hasFloorKey = true;
		Destroy(key);
		
		
		TextBox.Instance().UpdateText("You picked up a key....");
	}
	
	// private void OpenDoorDown(){
		// isDoorOpen = true;
		
		// Stairs.Instance().OpenDoors();
	// }
	
	private void Descend(){
		
		// Handle jumping to the next stage.
		
		isDoorOpen = false;
		hasFloorKey = false;
	}
	
	private bool CheckForWalls(){
		
		
		int layerMask = (int)(1<<9);
		layerMask = ~layerMask;
	
		float distance = startingLightRange;
		RaycastHit hit;
	
		Vector3 ray  = new Vector3(transform.position.x, transform.position.y, transform.lossyScale.z * .5f);
		if (Physics.Raycast(ray, transform.up, out hit)) {
			if (Vector3.Distance(transform.position, hit.point) <= distance && (hit.transform.tag == "Wall" || hit.transform.tag == "Door")){
				// Debug.Log(hit.transform.tag);
				lantern.GetComponent<Light>().range = Vector3.Distance(transform.position, hit.point) + 2f;
				return true;
			}

		}
		
		return false;
	}
	
	private string CheckFloor(){

		RaycastHit hit;
		
		// float distance = 1.1f;
		
		Vector3 ray = transform.position + new Vector3(horizontalDirection *.35f, verticalDirection * .35f, -.2f);
		if (Physics.Raycast(ray, Vector3.forward, out hit)){
			return hit.transform.tag;
		}
		
		return "";
	}
	
	private void CheckForSmokeEnemy()
	{
		//Added this for the stunning of SmokeEnemy 
		RaycastHit hit;
		
		Vector3 ray  = new Vector3(transform.position.x, transform.position.y, transform.position.z );
		
		if (Physics.Raycast(ray, transform.up, out hit, 5f)) 
		{
			if ((hit.transform.tag == "SmokeEnemy"))
			{
				hit.transform.GetComponent<SmokeEnemy>().isHitByLight = true;
			}
			else
			{
			}
		}
		
	}
	
	private void Death(){
		// Reset the player to "spawn point" or entrance to room
	}
	
	private void Finish(){
		// End state of game
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
			main.transform.position = new Vector3(main.transform.position.x, transform.position.y - cameraOffset, main.transform.position.z);
		} else if(transform.position.y  <  main.transform.position.y - cameraOffset){
			main.transform.position = new Vector3(main.transform.position.x, transform.position.y + cameraOffset, main.transform.position.z);
		}
	}
	
	public void GetFirstLight(){
		transform.Find("Lantern").GetComponent<Light>().enabled = true;
		GameObject.Find("LanternPickup").GetComponent<Light>().enabled = false;
	}
	
	public void ChangeLights(int light){
		// Handle switching between lights
	
	}
	
	public void SpotLantern(){
		TextBox.Instance().UpdateText("There is a lantern on the floor.");
	}
	
	
	// private static Player instance = null;
	
	// public static Player Instance(){
		// if (instance == null)
			// instance = GameObject.FindObjectOfType<Player>();
			
		// return instance;
	// }
}
