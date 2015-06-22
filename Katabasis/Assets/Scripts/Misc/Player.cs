using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float movementSpeed;
	
	private float startingLightRange;
	private float gravity;
	
	private int verticalDirection;
	private int horizontalDirection;
	
	public int playerDirection;

	public bool hasDiamond;
	public bool hasSapphire;
	public bool hasEmerald;
	public bool hasGold;
	public bool hasFloorKey;
	public bool isWalking;
	public bool hasLantern;
	public bool hasLens;
	public bool hasLaser;
	
	private bool holdingBox;
	// private bool grounded;

	private Transform position;
	private Transform lensTransform;
	private Transform lanternTransform;
	private Transform laserTransform;

	public GameObject activeLight;
	public GameObject lantern;
	public GameObject lens;
	public GameObject laser;

	private SpriteRenderer sprite;
	
	public Vector3 teleportLocation;
	
	private Vector3 playerForward;

	public Sprite back;
	public Sprite front;
	public Sprite left;
	public Sprite right;
	
	private Animator playerAnimator;
	
	public void Awake(){
		hasFloorKey = false;
		
		lens = transform.Find ("Lens").gameObject;
		laser = transform.Find ("Laser").gameObject;
		lantern = transform.Find ("Lantern").gameObject;
		sprite = transform.Find ("Sprite").gameObject.GetComponent<SpriteRenderer>();
		
		// laserTransform = transform.Find ("Laser");
		// laser = laserTransform.gameObject;
		
		activeLight = lantern;

		startingLightRange = lantern.GetComponent<Light>().range;
		// startingLightIntensity = lantern.GetComponent<Light>().intensity;
		
		playerAnimator = transform.Find("Animator").gameObject.GetComponent<Animator>();
		
		position = transform;
		
		// grounded = false;
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
		Debug.Log(floorCast);
		
		if(PassableTerrain(floorCast)){
			// Debug.Log("Moving");
			Move();
		} else if(floorCast == "LowerBoundary"){
			Teleport();
			// Debug.Log("Tele");
		}else {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
	
	public void Update(){
		// Figure out how to jump between lights
		if (Input.GetKeyUp ("1") && hasLantern) // && !laser.GetComponent<RedLight>().isFiring)
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
			activeLight.gameObject.SetActive (false);
			activeLight = lantern;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyUp ("2") && hasLens && activeLight != lens) // && !laser.GetComponent<RedLight>().isFiring)
		{
			// if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				// PurpleLight.Instance().LensOff();
			// }
			activeLight.gameObject.SetActive (false);
			activeLight = lens;
			activeLight.gameObject.SetActive (true);
		}
		
		if (Input.GetKeyUp ("3") && hasLaser) // && !laser.GetComponent<RedLight>().isFiring)
		{
			if(activeLight == lens && PurpleLight.Instance().revealedObjects != null){
				PurpleLight.Instance().LensOff();
			}
		
			activeLight.gameObject.SetActive (false);
			activeLight = laser;
			activeLight.gameObject.SetActive (true);
		}

		if (Input.GetKeyUp ("e"))
		{
			Activate();
		}	
		
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
		playerAnimator.gameObject.SetActive(isWalking);
		
		if(isWalking){
			playerAnimator.SetInteger("Direction", playerDirection);
			playerAnimator.SetBool("isWalking", isWalking);
		}
		
		if (holdingBox)
		{
			transform.FindChild ("Box").GetComponent<Rigidbody>().velocity = new Vector3(horizontalDirection * movementSpeed, verticalDirection * movementSpeed, 0f);
		}
	}
	
	private void Activate()
	{
		RaycastHit hit;
		Vector3 ray = new Vector3(transform.Find("Collider").position.x, transform.Find("Collider").position.y, transform.position.z); //Vector3.zero;
		
		float radius = 1f;
		
		Vector3 direction = Vector3.zero;
		switch(playerDirection){
			case(0):
				direction = -transform.up;
				break;
			case(1):
				direction = -transform.right;
				break;
			case(2):
				direction = transform.up;
				break;
			case(3):
				direction = transform.right;
				break;
			default:
				break;
		}
		
		// Shoot a ray from 
		
		if (Physics.SphereCast(ray - direction, radius, direction, out hit, 1f))
		{
			
			  switch (hit.transform.tag)
			  {
			  	case ("Box"):
			  		GrabOrDropBox(hit.transform.gameObject);
			  		break;
			  	case ("NPC"):
					Debug.Log("NPC!");
			  		hit.transform.gameObject.GetComponent<NPC>().Talk();
			  		break;
			  	case ("Mirror"):
			  		hit.transform.gameObject.GetComponent<Mirror>().RotateMirror();
			  		break;
			  	case ("MAGLaser"):
					
			  		MAGLaser.Instance().Activate();
			  		// Debug.Log("activate mag");
			  		// StartCoroutine(hit.transform.GetComponent<MAGLaser>().Fire());
			  		break;
			  	case ("LightBeacon"):
			  		hit.transform.gameObject.GetComponent<LightBeacon>().TakeLight();
			  		break;
				case("Diamond"):
					// Debug.Log("Hello");
					hasDiamond = true;
					GrabMineral(hit.transform.gameObject);
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
			o.transform.SetParent(transform);
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
	
	private void GrabMineral(GameObject m){
		m.GetComponent<MineralBlock>().FollowPlayer();
	}
	
	private void PickUpKey(GameObject key){
			
		
		hasFloorKey = true;
		
		key.GetComponent<AudioSource>().Play();
		
		Destroy(key);
		
		
		TextBox.Instance().UpdateText("You picked up a key....");
		
		
	}
	
	public void Descend(){
		
		Gameplay.Instance().NextLevel();
		
		// isDoorOpen = false;
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
		
		Vector3 ray = transform.Find("Collider").position + new Vector3(horizontalDirection *.35f, verticalDirection * .35f, transform.position.z);
		
		if (Physics.Raycast(ray, Vector3.forward, out hit)){
		
			if(hit.transform.parent.parent && hit.transform.parent.parent.tag == "MovingPlatform"){
				transform.parent = hit.transform.parent;
			} else {
				transform.parent = null;
			}
			
			if (hit.transform.gameObject.name == "Conveyor Belt" || hit.transform.gameObject.name == "Box" || hit.transform.gameObject.name == "FloorSwitch" || hit.transform.gameObject.name == "InvisibleFloorSwitch" || hit.transform.tag == "NPC" || hit.transform.gameObject.name == "Diamond")
			{
				// Debug.Log("Raycast Starts Here: " + ray + ". And it hits here: " + hit.point);
				// How do we make the conveyor belt move the player
				return hit.transform.tag;

			} else {
				// Debug.Log("Raycast Starts Here: " + ray + ". And it hits here: " + hit.point);
				return hit.transform.parent.tag;
			}
			
		}
		
		return "";
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
		// float cameraTiltAdjustment = -3;
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
	
	public void SpotLantern(){
		TextBox.Instance().UpdateText("There is a lantern on the floor.");
	}
	
	public void Teleport()
	{
		transform.position = new Vector3(teleportLocation.x, teleportLocation.y, transform.position.z);
		Gameplay.Instance().spawnLocation.gameObject.GetComponent<AudioSource>().Play();
	}
	
	private bool PassableTerrain(string s){
		return s == "Floor" || s == "FloorSwitch" || s == "InvisibleFloor" || s == "Box" || s == "WallSwitch" || s == "Statue" || s == "ConveyorBelt" || s == "MovingPlatform" || s == "Key" || s == "StairsDown" || s == "NPC";
	}
	
	// private static Player instance = null;
	
	// public static Player Instance(){
		// if (instance == null)
			// instance = GameObject.FindObjectOfType<Player>();
			
		// return instance;
	// }
}
