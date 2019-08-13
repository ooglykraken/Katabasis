using UnityEngine;
using System.Collections;

public class VanishingMonster : MonoBehaviour {
	
	private float detectionDistance = 5f;
	private float speed = 3f;
	public float lifeLeft;
	private float fadeAdjust = .008f;
	
	private static float lifetime = 400f;
	
	private bool isDetected;
	private bool isFading;
	private bool isRunning;
	private bool isChasing;
	
	private SpriteRenderer sprite;
	
	private Transform player;
	
	public void Awake(){
		sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
		isDetected = false;
		isFading = false;
		isRunning = false;
		isChasing = false;
		
		player = Player.Instance().transform;
	}
	
	public void Update(){
		
		if(isDetected){
			lifeLeft--;
			if(lifeLeft <= 0f){
				Disappear();
			}
			if(isFading){
				Fading();
			}
		}
		
		if(!isDetected && Vector3.Distance(transform.position, player.position) <= detectionDistance){
			React();
		}
		
		if(isChasing){
			Chase();
		}
		if(isRunning){
			Runaway();
		}
	}
	
	public void OnTriggerEnter(Collider c){
		Debug.Log(c);
		if(c.transform.parent.tag == "Player"){
			Disappear(); 
		}
	}
	
	private void React(){
		switch(Random.Range(0, 2)){
			case 0:
				isRunning = true;
				Runaway();
				break;
			case 1:
				isChasing = true;
				Chase();
				break;
			default:
				Disappear();
				break;
		}
		
		isFading = true;
		isDetected = true;
		lifeLeft = lifetime;
	}
	
	private void Chase(){
		transform.position = Vector3.MoveTowards(transform.position, player.Find("Collider").position, Time.deltaTime * speed);
	}
	
	public void Disappear(){
		SpriteOrderer.Instance().allSpriteRenderers.Remove(sprite);
		Destroy(gameObject);
	}
	
	private void Runaway(){
		transform.position = Vector3.MoveTowards(transform.position, player.Find("Collider").position, Time.deltaTime * -speed);
	}
	
	private void Fading(){
		if(sprite.material.color.a > 0f){
			// Debug.Log("fading out");
			sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, lifeLeft/lifetime);
		}
	}
}
