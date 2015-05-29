using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float movementSpeed;
	private float startingLightRange;
	
	private int verticalDirection;
	private int horizontalDirection;
	
	private Transform position;
	
	private Vector3 playerForward;
	private int playerDirection;
	
	public bool hasFloorKey;
	public bool isWalking;
	private bool isDoorOpen;
<<<<<<< HEAD
	
=======
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
	public bool hasLantern;
	public bool hasLens;
	public bool hasLaser;
	private bool holdingBox;
<<<<<<< HEAD
=======
	
	private bool isSlowed;
	private Transform lensTransform;
	private Transform lanternTransform;
	private Transform laserTransform;
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
	
	public GameObject activeLight;
	private GameObject lantern;
	private GameObject lens;
	private GameObject laser;
<<<<<<< HEAD

=======
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
	
	private SpriteRenderer sprite;
	
	public Vector3 teleportLocation;
<<<<<<< HEAD
=======

>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
	
	public Sprite back;
	public Sprite front;
	public Sprite left;
	public Sprite right;
	
	public void Awake(){
		hasFloorKey = false;
		
		lens = transform.Find ("Lens").gameObject;
		laser = transform.Find ("Laser").gameObject;
		lantern = transform.Find ("Lantern").gameObject;
		sprite = transform.Find ("Sprite").gameObject.GetComponent<SpriteRenderer>();
<<<<<<< HEAD
=======
		
		// laserTransform = transform.Find ("Laser");
		// laser = laserTransform.gameObject;
		
		activeLight = lantern;
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab

		startingLightRange = lantern.GetComponent<Light>().range;
		// startingLightIntensity = lantern.GetComponent<Light>().intensity;
		
		position = transform;
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
				Gameplay.Instance().FinishGame();
			} else {
			}
		}
		
	}
	
	public void OnCollisionStay(Collision c)
	{
		if (c.transform.name.Equals ("PurpleLightFloor"))
		{
			gameObject.transform.SetParent(c.transform.parent);
			
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

		if(floorCast == "Floor"  || floorCast == "FloorSwitch" || floorCast == "InvisibleFloor" || floorCast == "Box" || floorCast == "WallSwitch" || floorCast == "Statue" || floorCast == "ConveyorBelt"){
			// Debug.Log("Im moving");
		
			Move();
		} else if(floorCast == "LowerBoundary"){
			Teleport();
		}else {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		
		// LoseLight();
		
		// if(!CheckForWalls() ){
			// lantern.GetComponent<Light>().range = startingLightRange;
		// }
		
<<<<<<< HEAD
	}
	
	public void Update(){
		ChangeLights();
=======
// <<<<<<< HEAD
	}
	
	public void Update(){
		// Figure out how to jump between lights
		if (Input.GetKeyUp ("1") && hasLantern && !laser.GetComponent<RedLight>().isFiring)
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
			activeLight.gameObject.SetActive (false);
			activeLight = lantern;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyUp ("2") && hasLens && activeLight != lens && !laser.GetComponent<RedLight>().isFiring)
		{
			// if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				// PurpleLight.Instance().LensOff();
			// }
			activeLight.gameObject.SetActive (false);
			activeLight = lens;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyUp ("3") && hasLaser && !laser.GetComponent<RedLight>().isFiring)
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
		
			activeLight.gameObject.SetActive (false);
			activeLight = laser;
			activeLight.gameObject.SetActive (true);
		}
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab

		if (Input.GetKeyUp ("e"))
		{
			Activate();
		}

		Animator playerAnimator = transform.Find("Animator").gameObject.GetComponent<Animator>();
		playerAnimator.SetInteger("Direction", playerDirection);
		
		if(Gameplay.Instance().spawnLocation != null){
			teleportLocation = Gameplay.Instance().spawnLocation.position;
		}
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
	
		GetComponent<Rigidbody>().velocity = new Vector3(horizontalDirection * movementSpeed, verticalDirection * movementSpeed, 0f);
		//GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime);
		
		if(horizontalDirection == 0 && verticalDirection == 0){
			gameObject.GetComponent<AudioSource>().Stop();
			isWalking = false;
		} else {
			if(!isWalking){
				gameObject.GetComponent<AudioSource>().Play();
			}
			isWalking = true;
		}
		
		sprite.enabled = !isWalking;
		transform.Find("Animator").gameObject.SetActive(isWalking);
		transform.Find("Animator").gameObject.GetComponent<Animator>().SetBool("isWalking", isWalking);
		
		if (holdingBox)
		{
			transform.FindChild ("Box").GetComponent<Rigidbody>().velocity = new Vector3(horizontalDirection * movementSpeed, verticalDirection * movementSpeed, 0f);
		}
	}
	
	private void Activate()
	{
		RaycastHit hit;
		
		if (Physics.Raycast(transform.position, playerForward, out hit, 1.5f))
		{
			  switch (hit.transform.tag)
			  {
			  	case ("Box"):
			  		GrabOrDropBox(hit.transform.gameObject);
			  		break;
			  	case ("NPC"):
			  		hit.transform.gameObject.GetComponent<NPC>().Talk();
			  		break;
			  	case ("Mirror"):
			  		hit.transform.gameObject.GetComponent<Mirror>().RotateMirror();
			  		break;
			  	case ("MAGLaser"):
<<<<<<< HEAD
			  		StartCoroutine(hit.transform.GetComponent<MAGLaser>().Fire());
=======
			  		Debug.Log("activate mag");
			  		// StartCoroutine(hit.transform.GetComponent<MAGLaser>().Fire());
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
			  		break;
			  	case ("LightBeacon"):
			  		hit.transform.gameObject.GetComponent<LightBeacon>().TakeLight();
			  		break;
				default:
					break;
			  }
		}
	}
	
	private void GrabOrDropBox(GameObject o)
	{
		if (!holdingBox)
		{
			o.transform.SetParent(gameObject.transform);
			holdingBox = true;
		}
		else
		{
			if (transform.FindChild("Box"))
			{
				transform.FindChild ("Box").SetParent(null);
				holdingBox = false;
			}
		}
	}
	
	
	private void PickUpKey(GameObject key){
			
		
		hasFloorKey = true;
		
		key.GetComponent<AudioSource>().Play();
		
		Destroy(key);
		
		
		TextBox.Instance().UpdateText("You picked up a key....");
		
		
	}
	
	public void Descend(){
		
		Gameplay.Instance().NextLevel();
		
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
			if(hit.transform.parent && hit.transform.parent.tag == "MovingPlatform"){
				transform.parent = hit.transform.parent;
			} else {
				transform.parent = null;
			}
			if (hit.transform.gameObject.name == "ConveyorBelt")
			{
				gameObject.GetComponent<Rigidbody>().velocity += hit.transform.up * 15f *Time.deltaTime;
			}
			return hit.transform.tag;
		}
		
		return "";
	}
	
	private void Death(){
		// Reset the player to "spawn point" or entrance to room
	}
	
	private void Turn(string direction){
		// Before this was to turn the boy it is now repurposed to change the direction of the lantern
	
		Vector3 newFacing = new Vector3(0f, 0f, 0f);
		
		// Sprite playerSprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
		
		switch(direction){
			case "up":
				newFacing = new Vector3(270f, 90f, 0f);
				playerForward = new Vector3(0f, 1f, 0f);
				sprite.sprite = back;
				playerDirection = 2;
				break;
			case "down":
				newFacing = new Vector3(90f, 90f, 0f);
				playerForward = new Vector3(0f, -1f, 0f);
				sprite.sprite = front;
				playerDirection = 0;		
				break;
			case "left":
				newFacing = new Vector3(180f, 90f, 0f);
				playerForward = new Vector3(-1f, 0f, 0f);
				sprite.sprite = left;
				playerDirection = 1;
				break;
			case "right":
				newFacing = new Vector3(0f, 90f, 0f);
				playerForward = new Vector3(1f, 0f, 0f);
				sprite.sprite = right;
				playerDirection = 3;
				break;
			case "r-d":
				// newFacing = new Vector3(0f, 0f, 225f);
				newFacing = lantern.transform.eulerAngles;
				break;
			case "l-d":
				// newFacing = new Vector3(0f, 0f, 135f);
				
				newFacing = lantern.transform.eulerAngles;
				break;
			case "r-u":
				// newFacing = new Vector3(0f, 0f, 325f);
				
				newFacing = lantern.transform.eulerAngles;
				break;
			case "l-u":
				// newFacing = new Vector3(0f, 0f, 45f);
				
				newFacing = lantern.transform.eulerAngles;
				break;
			default:
				break;
		}
		lantern.transform.eulerAngles = newFacing;
		laser.transform.eulerAngles = newFacing;
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
		
		hasLantern = true;
		
		Destroy(GameObject.Find("LanternPickup"));
		// GameObject.Find("LanternPickup").GetComponent<Light>().enabled = false;
	}
	
	public void ChangeLights(){
<<<<<<< HEAD
		// Handle switching between lights
		
		if (Input.GetKeyUp ("1") && hasLantern && !laser.GetComponent<RedLight>().isFiring)
=======
// <<<<<<< HEAD
		// Handle switching between lights
		
// =======
		if (Input.GetKeyDown ("1"))
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
			activeLight.gameObject.SetActive (false);
			activeLight = lantern;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyUp ("2") && hasLens && activeLight != lens && !laser.GetComponent<RedLight>().isFiring)
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
			activeLight.gameObject.SetActive (false);
			activeLight = lens;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyUp ("3") && hasLaser && !laser.GetComponent<RedLight>().isFiring)
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
			activeLight.gameObject.SetActive (false);
			activeLight = laser;
			activeLight.gameObject.SetActive (true);
		}
	
	}
	
	public void SpotLantern(){
		TextBox.Instance().UpdateText("There is a lantern on the floor.");
	}
	
	public void Teleport()
	{
		transform.position = teleportLocation;
		Gameplay.Instance().spawnLocation.gameObject.GetComponent<AudioSource>().Play();
	}
	
	// private static Player instance = null;
	
	// public static Player Instance(){
		// if (instance == null)
			// instance = GameObject.FindObjectOfType<Player>();
			
		// return instance;
	// }
}
