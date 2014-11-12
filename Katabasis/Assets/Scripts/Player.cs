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
	private bool isUsingPointer;
	public bool isWalking;
	private bool isDoorOpen;
	
	private Transform lanternTransform;
	
	private GameObject lantern;
	private GameObject dpadCenter;
	
	public void Awake(){
		hasFloorKey = false;
		
		lanternTransform = transform.Find("Lantern");
		lantern = lanternTransform.gameObject;
		
		startingLightRange = lantern.GetComponent<Light>().range;
		startingLightIntensity = lantern.GetComponent<Light>().intensity;
		
		isUsingPointer = !Settings.Instance().isUsingDpad;
		
		dpadCenter = Instantiate(Resources.Load("UI/DpadCenter", typeof(GameObject)) as GameObject) as GameObject;
		dpadCenter.GetComponent<Renderer>().enabled = false;
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
	}
	
	public void Update(){
		#if UNITY_ANDROID
			if(!isUsingPointer){
				DPad();
			} else {
				PointerMove();
			}
		#endif
	}
	
	public void FixedUpdate(){
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			verticalDirection = (int)Input.GetAxisRaw("Vertical");
			horizontalDirection = (int)Input.GetAxisRaw("Horizontal");
		#endif
		
		string floorCast = CheckFloor();
		if(floorCast == "Floor"  || floorCast == "FloorSwitch" || floorCast == "InvisibleFloor"){
			Move();
		} else {
			rigidbody.velocity = Vector3.zero;
		}
		
		LoseLight();
		
		if(!CheckForWalls() ){
			lantern.GetComponent<Light>().range = startingLightRange;
		}
	}
	
	public void LateUpdate(){
		CameraFollow();
	}
	
	private void DPad(){
		float dpadSensitivity = 2f;
		
		Touch touch = Input.GetTouch(0);
		Vector2 fingerStartingPosition = Vector2.zero;
		
		if(touch.phase == TouchPhase.Began){
			fingerStartingPosition = touch.position;
			
			dpadCenter.GetComponent<Renderer>().enabled = true;
			dpadCenter.transform.position = Camera.main.ScreenToWorldPoint(fingerStartingPosition) + new Vector3(0f, 0f, -2f);
		} else if(touch.phase == TouchPhase.Moved){
			if(touch.deltaPosition.x + fingerStartingPosition.x > fingerStartingPosition.x + dpadSensitivity){
				horizontalDirection = 1;
			} else if(touch.deltaPosition.x + fingerStartingPosition.x < fingerStartingPosition.x - dpadSensitivity){
				horizontalDirection = -1;
			}
			if(touch.deltaPosition.y + fingerStartingPosition.x > fingerStartingPosition.x + dpadSensitivity){
				verticalDirection = 1;
			} else if(touch.deltaPosition.y + fingerStartingPosition.x < fingerStartingPosition.x - dpadSensitivity){
				verticalDirection = -1;
			} 
		} else if(touch.phase == TouchPhase.Ended){
			fingerStartingPosition = Vector2.zero;
			horizontalDirection = 0;
			verticalDirection = 0;
			
			dpadCenter.GetComponent<Renderer>().enabled = false;
		}
	}
	
	private void PointerMove(){
		float sensitivity = 3.5f;
		Touch touch = Input.GetTouch(0);
		
		Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
		if(touch.phase == TouchPhase.Began){
			// Debug.Log(touch.position.x + "   " + transform.position.x); 
			if(touchPosition.x > transform.position.x + sensitivity){
				horizontalDirection = 1;
			} else if(touchPosition.x < transform.position.x - sensitivity){
				horizontalDirection = -1;
			}
			if(touchPosition.y > transform.position.y + sensitivity){
				verticalDirection = 1;
			} else if(touchPosition.y < transform.position.y - sensitivity){
				verticalDirection = -1;
			}
		} else if(touch.phase == TouchPhase.Ended){
			horizontalDirection = 0;
			verticalDirection = 0;
		}
		
		// transform.eulerAngles = Vector3.RotateTowards(transform.up, Camera.main.ScreenToWorldPoint(touch.position),  step * Time.deltaTime, 0f);
		// rigidbody.velocity = movementSpeed * transform.up;
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
		if(Gameplay.Instance().currentLevel <= Gameplay.Instance().finalLevel){
			
			Gameplay.Instance().NextLevel();
			lantern.GetComponent<Light>().intensity = startingLightIntensity;
		} else {
			//Finish();
		}
		
		isDoorOpen = false;
		hasFloorKey = false;
	}
	
	private void LoseLight(){
		
		lantern.GetComponent<Light>().intensity -= lightLostPerFrame;
		lantern.GetComponent<Light>().spotAngle = lantern.GetComponent<Light>().intensity * 10f;
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
				lantern.GetComponent<Light>().range = Vector3.Distance(transform.position, hit.point);
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
