using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	private int laserDuration = 30;
	public int laserTimer;
	
	private int laserRange = 8;
	
	private float beamSpeed = 1500f;
	
	private SpriteRenderer laserSprite;
	private SpriteRenderer beamSprite;
	
	private Animator laserAnimator;
	private Animator beamAnimator;
	
	private Rigidbody beamProjectile;
	
	private GameObject beam;
	
	public void Awake(){
		beam = transform.Find("Beam").gameObject;
	
		laserSprite = transform.Find("Animator").gameObject.GetComponent<SpriteRenderer>();
		beamSprite = beam.GetComponent<SpriteRenderer>();
		laserSprite.enabled = false;
		beamSprite.enabled = false;
		
		laserAnimator = transform.Find("Animator").gameObject.GetComponent<Animator>();
		beamAnimator = beam.GetComponent<Animator>();
		
		beamProjectile = beam.GetComponent<Rigidbody>();
	}
	
	public void Update(){
		
		if(laserTimer > 0 ){
			laserTimer--;
			MoveBeam();
		}
		
		if(laserTimer == 0){
			laserSprite.enabled = false;
			beamSprite.enabled = false;
			laserAnimator.SetBool("firing", false);
			beamAnimator.SetBool("firing", false);
			beamProjectile.velocity = Vector3.zero;
			beam.transform.localPosition = new Vector3(0f, 0f, 2f);
		}
		// if(Input.GetKeyDown("space") && (laserTimer == laserDuration/2 || laserTimer == 0)){
			
			// Fire();
			
		// }
	}
	
	public void Fire(){
		EnableBeam();
	
		int distance = laserRange;
		RaycastHit[] hits;
		
		Vector3 positionModifier = Vector3.zero;
		switch(transform.parent.gameObject.GetComponent<Player>().playerDirection){
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
		
		Vector3 colliderPosition = Player.Instance().transform.Find("Collider").position;
		
		Vector3 ray = new Vector3(colliderPosition.x, colliderPosition.y, Player.Instance().transform.position.z);
		
		// RaycastHit[] 
		
		hits = Physics.SphereCastAll(ray, .4f, transform.forward, distance);
		
		foreach(RaycastHit h in hits){
			// Debug.Log(h.transform.parent.tag);
			// Debug.DrawRay(ray - positionModifier, transform.forward, Color.green, distance);
			if(h.transform.tag == "Box"){
				// Debug.Log("IT'S A BEAM!");
				// laserTimer = laserDuration;
				// beamMesh.enabled = true;
				// GetComponent<AudioSource>().Play();
				// Debug.Log(h.transform.tag);
				h.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().enabled = false;
				h.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
			} else if(h.transform.tag == "Monster"){
			
				Debug.Log("Enemy Hit");
				h.transform.gameObject.GetComponent<DepMonsterAI>().Kill();
			} else if(h.transform.tag == "VanishingMonster"){
				h.transform.gameObject.GetComponent<VanishingMonster>().Disappear();
			} else if(h.transform.parent == null){
				// catch gameobjects without parents before checking their tag to prevent breaking
			} else if(h.transform.parent.tag == "Breakable"){
				GameObject g = h.transform.parent.gameObject;
				// g.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
				// g.transform.Find("Model").gameObject.GetComponent<MeshRenderer>().enabled = false;
				Destroy(g);
			} else if(h.transform.parent.tag == "Door"){
				GameObject g = h.transform.parent.gameObject;
				
				// SpriteOrderer.Instance().allSpriteRenderers.Remove(h.transform.parent.Find("Model").gameObject.GetComponent<Renderer>());
				// SpriteOrderer.Instance().allSpriteRenderers.Remove(h.transform.parent.Find("Model/Wall Crack").gameObject.GetComponent<SpriteRenderer>());
				
				Destroy(g);
			}else if(h.transform.parent.tag == "Wall"){
				GameObject g = h.transform.parent.gameObject;
				
				SpriteOrderer.Instance().allSpriteRenderers.Remove(h.transform.parent.Find("Model").gameObject.GetComponent<SpriteRenderer>());
				SpriteOrderer.Instance().allSpriteRenderers.Remove(h.transform.parent.Find("Model/Wall Crack").gameObject.GetComponent<SpriteRenderer>());
				
				Destroy(g);
			} else {
			}
		}
	}
	
	public void EnableBeam(){
			laserAnimator.SetBool("firing", true);
			beamAnimator.SetBool("firing", true);
			laserSprite.enabled = true;
			beamSprite.enabled = true;
			GetComponent<AudioSource>().Play();
			laserTimer = laserDuration;
	}
	
	private void MoveBeam(){
		beamProjectile.velocity = transform.forward * beamSpeed * Time.deltaTime;
	}
	
	private static Laser instance = null;
	
	public static Laser Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<Laser>();
		}
		
		return instance;
	}
}
