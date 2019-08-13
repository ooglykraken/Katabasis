using UnityEngine;
using System.Collections;

public class DepMonsterAI : MonoBehaviour {

	public GameObject player;
	public GameObject spawnControl;
	
	private SpriteRenderer spriteRenderer;
	
	private float obstacleDetection;
	private float speed = 1.8f;
	private float speedModifier;
	
	private int invisibleDuration = 150;
	private int invisibleCounter;
	
	private int sprintDuration = 170;
	public int sprintDurationTimer;
	private int sprintCooldown = 400;
	public int sprintCooldownTimer;
	
	private new Collider collider;
	
	// private string lowHitTag;
	// private string highHitTag;
	
	private bool isInvisible;
	public bool isSprinting;
	public bool isDead;
	
	// private Vector3 proposedPos;
	// private Vector2 newPosition;
	
	public void Awake(){
		isDead = false;
		isInvisible = false;
		obstacleDetection = 5f;
		speedModifier = 1f;
		sprintCooldownTimer = sprintCooldown;
		player = Player.Instance().gameObject;
		
		spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		collider = transform.Find("Collider").gameObject.GetComponent<Collider>();
		
		
	}
	
	public void Update(){
		
		if(isDead){
			return;
		}
		
		// CheckPath();
		Move();
			
		if(isInvisible){
			invisibleCounter--;
			if(invisibleCounter <= 0){
				Invisible();
			}
		}
		
		if(isSprinting){
			sprintDurationTimer--;
			if(sprintDurationTimer <= 0){
				speedModifier = Sprint();
			}
		} else {
			sprintCooldownTimer--;
			if(sprintCooldownTimer <= 0 && !isInvisible){
				speedModifier = Sprint();
			}
		}
	}
	
	public void FixedUpdate()
	{
		
	}
	
	public void OnCollisionEnter(Collision c)
	{
		if (c.transform.tag == "Player")
		{
			spawnControl.GetComponent<SpawnandBeaconControl>().RemoveBeacon();
			Kill();
		} else if(c.transform.tag == "LightBeacon"){
			Invisible();
		}
	}
	
	private void Move(){
		transform.position = Vector3.MoveTowards(transform.position, player.transform.Find("Collider").position, Time.deltaTime * speed * speedModifier);
	}
	
	public void Respawn()
	{	
		isDead = false;
		
		
		Vector3 proposedPos =  Random.onUnitSphere * 16;
		while(Vector3.Distance(proposedPos, player.transform.position) < 14) {
			proposedPos =  Random.onUnitSphere * 16;
		}
		// Vector2 newPosition = proposedPos;
		transform.position = new Vector3(proposedPos.x, proposedPos.y, -.5f);
		spriteRenderer.enabled = true;
		collider.enabled = true;
		
		SpriteOrderer.Instance().allSpriteRenderers.Add(transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		
		// monsterIndex++;;
	}
	
	public void Kill(){
		isDead = true;
		
		spriteRenderer.enabled = false;
		collider.enabled = false;
	}
	
	private void Invisible(){
		if(collider.enabled){
			spriteRenderer.material.color = new Vector4(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, .2f);
			collider.enabled = false;
			// gameObject.GetComponent<Rigidbody>().enabled = false;
			isInvisible = true;
			invisibleCounter = invisibleDuration;
		} else {
			spriteRenderer.material.color = new Vector4(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1f);
			collider.enabled = true;
			// gameObject.GetComponent<Rigidbody>().enabled = true;
			isInvisible = false;
		}
	}
	
	private float Sprint(){
		if(speedModifier > 1f){
			isSprinting = false;
			
			sprintCooldownTimer = sprintCooldown;
			return 1f;
		} else {
			
			isSprinting = true;
			sprintDurationTimer = sprintDuration;
			return 2.1f;
		}
	}
	
	private bool CheckPath(){

		RaycastHit hit;
		Vector3 start = collider.ClosestPointOnBounds(player.GetComponent<Rigidbody>().position);
		Vector3 target = player.transform.position - start;
		Vector3 hitNormal;
		
		if(Physics.SphereCast(start, 1f, target,  out hit, obstacleDetection)){
			// hitNormal = hit.normal;
			Debug.Log(hit.transform.tag);
			// Debug.DrawRay(start, target, Color.green);
			return true;
		}
		
		return false;
	}
	
	
}
