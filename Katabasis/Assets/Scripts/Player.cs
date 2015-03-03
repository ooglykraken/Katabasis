using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float movementSpeed;
	// public float lightLostPerFrame = .001f;
	// private float startingLightIntensity;
	private float startingLightRange;
	
	private int verticalDirection;
	private int horizontalDirection;
	
	
	public bool hasFloorKey;
	public bool isWalking;
	private bool isDoorOpen;
	
	//Added for SmokeEnemy 
	// private bool isSlowed;
	private Transform lensTransform;
	private Transform lanternTransform;
	
	public GameObject activeLight;
	private GameObject lantern;
	private GameObject lens;
	public bool hasLens;
	
	public Transform teleportLocation;
	
	// public Texture2D boyFront;
	// public Texture2D boyBack;
	// public Texture2D boyLeft;
	// public Texture2D boyRight;
	
	public void Awake(){
		hasFloorKey = false;
		hasLens = false;
		
		//Added for SmokeEnemy slow effect
		// isSlowed = false;
		
		lensTransform = transform.Find ("Lens");
		lens = lensTransform.gameObject;
		
		lanternTransform = transform.Find("Lantern");
		lantern = lanternTransform.gameObject;
		
		activeLight = lantern;
		
		startingLightRange = lantern.GetComponent<Light>().range;
		// startingLightIntensity = lantern.GetComponent<Light>().intensity;
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
			// } else if(c.transform.parent.tag == "Enemy"){
				// Death();
			} else if(c.transform.parent.tag == "StairsUp"){
				Finish();
			} else {
			}
		}
		
	}
	
	public void OnCollisionStay(Collision c)
	{
		if (c.transform.name.Equals ("PurpleLightFloor"))
		{
			gameObject.transform.SetParent(c.transform);
			
		}
		
	}
	
	public void OnCollisionExit(Collision c)
	{
		gameObject.transform.SetParent(null);
	}
	
	public void FixedUpdate(){
		verticalDirection = (int)Input.GetAxisRaw("Vertical");
		horizontalDirection = (int)Input.GetAxisRaw("Horizontal");

		string floorCast = CheckFloor();
		if(floorCast == "Floor"  || floorCast == "FloorSwitch" || floorCast == "InvisibleFloor"){
			Move();
		} else {
			rigidbody.velocity = Vector3.zero;
		}
		
		// LoseLight();
		
		// if(!CheckForWalls() ){
			// lantern.GetComponent<Light>().range = startingLightRange;
		// }
		
		if (Input.GetKeyDown ("1"))
		{
			activeLight.gameObject.SetActive (false);
			activeLight = lantern;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyDown ("2") && hasLens == true)
		{
			activeLight.gameObject.SetActive (false);
			activeLight = lens;
			activeLight.gameObject.SetActive (true);
		}
		
		//Check the SmokeEnemy related stuff
		// Slowed();	
		// CheckForSmokeEnemy();
		
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
	
	// private void Slowed()
	// {
		// if (isSlowed == true){
			// movementSpeed = 2.5f;
			// lightLostPerFrame = .003f;
		// }else{
			// movementSpeed = 5f;
			// isSlowed = false;
			// lightLostPerFrame = .001f;
		// }
	// }
	
	private void PickUpKey(GameObject key){
			
		hasFloorKey = true;
		Destroy(key);
		
		
		TextBox.Instance().UpdateText("You picked up a key....");
	}
	
	// private void OpenDoorDown(){
		// isDoorOpen = true;
		
		// Stairs.Instance().OpenDoors();
	// }
	
	public void Descend(){
		
		// Handle jumping to the next stage.
		if(Application.loadedLevel != Gameplay.Instance().finalLevel){
			Application.LoadLevel(Application.loadedLevel + 1);
		} else {
			Finish();
		}
		isDoorOpen = false;
		hasFloorKey = false;
	}
	
	private bool CheckForWalls(){
		
		
		int layerMask = (int)(1<<9);
		layerMask = ~layerMask;
		
		// I'm increasing this by an arbitrary number to account for the bug where light doesn't readjust properly
		float distance = startingLightRange + 10000000f;
		RaycastHit hit;
	
		Vector3 ray  = new Vector3(transform.position.x, transform.position.y, transform.lossyScale.z * .5f);
		if (Physics.Raycast(ray, transform.up, out hit)) {
			if (Vector3.Distance(transform.position, hit.point) <= distance && (hit.transform.tag == "Wall" || hit.transform.tag == "Door")){
				Debug.Log(hit.transform.tag);
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
	
	// private void CheckForSmokeEnemy()
	// {
		// Added this for the stunning of SmokeEnemy 
		// RaycastHit hit;
		
		// Vector3 ray  = new Vector3(transform.position.x, transform.position.y, transform.position.z );
		
		// if (Physics.Raycast(ray, transform.up, out hit, 5f)) 
		// {
			// if ((hit.transform.tag == "SmokeEnemy"))
			// {
				// hit.transform.GetComponent<SmokeEnemy>().isHitByLight = true;
			// }
			// else
			// {
			// }
		// }
		
	// }
	
	private void Death(){
		// Reset the player to "spawn point" or entrance to room
	}
	
	private void Finish(){
		// End state of game
	}
	
	private void Turn(string direction){
		// Before this was to turn the boy it is now repurposed to change the direction of the lantern
	
		Vector3 newFacing = new Vector3(0f, 0f, 0f);
		
		// Sprite playerSprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
		
		switch(direction){
			case "up":
				newFacing = new Vector3(270f, 90f, 0f);
				
				// playerSprite = boyBack;
				
				foreach(Transform t in transform){
					if(t.tag == "Sprite"){
						t.gameObject.SetActive(false);
					}
				}
				
				transform.Find("SpriteBack").gameObject.SetActive(true);
				break;
			case "down":
				newFacing = new Vector3(90f, 90f, 0f);

				// playerMaterial.mainTexture = boyFront;	
				
				foreach(Transform t in transform){
					if(t.tag == "Sprite"){
						t.gameObject.SetActive(false);
					}
				}
				
				transform.Find("SpriteFront").gameObject.SetActive(true);				
				break;
			case "left":
				newFacing = new Vector3(180f, 90f, 0f);
				
				// playerMaterial.mainTexture = boyLeft;
				
				foreach(Transform t in transform){
					if(t.tag == "Sprite"){
						t.gameObject.SetActive(false);
					}
				}
				
				transform.Find("SpriteLeft").gameObject.SetActive(true);
				break;
			case "right":
				newFacing = new Vector3(0f, 90f, 0f);
				
				// playerMaterial.mainTexture = boyRight;
				
				foreach(Transform t in transform){
					if(t.tag == "Sprite"){
						t.gameObject.SetActive(false);
					}
				}
				
				transform.Find("SpriteRight").gameObject.SetActive(true);
				break;
			case "r-d":
				// newFacing = new Vector3(0f, 0f, 225f);
				newFacing = transform.Find("Lantern").eulerAngles;
				break;
			case "l-d":
				// newFacing = new Vector3(0f, 0f, 135f);
				
				newFacing = transform.Find("Lantern").eulerAngles;
				break;
			case "r-u":
				// newFacing = new Vector3(0f, 0f, 325f);
				
				newFacing = transform.Find("Lantern").eulerAngles;
				break;
			case "l-u":
				// newFacing = new Vector3(0f, 0f, 45f);
				
				newFacing = transform.Find("Lantern").eulerAngles;
				break;
			default:
				break;
		}
		
		transform.Find("Lantern").eulerAngles = newFacing;
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
		transform.Find("Lantern").gameObject.SetActive(true);
		
		Destroy(GameObject.Find("LanternPickup"));
		// GameObject.Find("LanternPickup").GetComponent<Light>().enabled = false;
	}
	
	public void ChangeLights(int light){
		// Handle switching between lights
	
	}
	
	public void SpotLantern(){
		TextBox.Instance().UpdateText("There is a lantern on the floor.");
	}
	
	public void Teleport()
	{
		transform.position = teleportLocation.position;
	}
	
	// private static Player instance = null;
	
	// public static Player Instance(){
		// if (instance == null)
			// instance = GameObject.FindObjectOfType<Player>();
			
		// return instance;
	// }
}
