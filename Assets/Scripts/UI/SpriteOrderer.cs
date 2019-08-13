using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteOrderer : MonoBehaviour {
	
	private static int backLayer = 0;
	private static int playerBoxLayer = 1;
	private static int playerLayer = 2;
	private static int frontLayer = 3;
	private static int wallCrackLayer = 4;
		
	private GameObject player;
	
	public List<SpriteRenderer> allSpriteRenderers = new List<SpriteRenderer>();
	
	public void Awake(){
		player = Player.Instance().gameObject;
		
		player.transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = playerLayer;
		player.transform.Find("Animator").GetComponent<SpriteRenderer>().sortingOrder = playerLayer;
		
		foreach(GameObject g in FindObjectsOfType<GameObject>()){
			if(g.transform.tag != "WallSwitch" && g.transform.tag != "FloorSwitch" && g.transform.tag != "Key"){
			
				Transform currentSprite = g.transform.Find("Sprite");
			
				if(currentSprite && currentSprite != player.transform.Find("Sprite")){
					allSpriteRenderers.Add(currentSprite.gameObject.GetComponent<SpriteRenderer>());
				}
			}
		}
	}
	
	public void Update(){
		foreach(SpriteRenderer s in allSpriteRenderers){
			if(s.transform.parent.tag == "FloorSwitch" || s.gameObject.transform.parent.tag == "WallSwitch"){
				s.sortingOrder = backLayer;
			} else if(s.transform.parent.tag == "Door"){
				// Debug.Log(s.gameObject.transform.parent.eulerAngles.z);
				
				if((s.gameObject.transform.parent.eulerAngles.z > 265f && s.gameObject.transform.parent.eulerAngles.z < 275f) || (s.gameObject.transform.parent.eulerAngles.z > 85f && s.gameObject.transform.parent.eulerAngles.z < 95f)){
					if(player.transform.position.y > s.transform.parent.Find("Collider").gameObject.GetComponent<Collider>().bounds.min.y){
						s.sortingOrder = backLayer;
					} else if(player.transform.position.y < s.bounds.min.y){
						s.sortingOrder = frontLayer;
					} else {
						s.sortingOrder = playerLayer;
					}
				} else {
					if(player.transform.position.y > s.transform.parent.position.y){
						s.sortingOrder = frontLayer;
					} else if(player.transform.position.y < s.transform.parent.position.y){
						s.sortingOrder = backLayer;
					} else {
						s.sortingOrder = playerLayer;
					}
				}
				
				if(s.transform.Find("Wall Crack")){
					Debug.Log("Wall crack");
					s.transform.Find("Wall Crack").gameObject.GetComponent<SpriteRenderer>().sortingOrder = wallCrackLayer;
				}
			}else {
		
				if(player.transform.position.y > s.transform.parent.position.y){
					s.sortingOrder = frontLayer;
				} else if(player.transform.position.y < s.transform.parent.position.y){
					s.sortingOrder = backLayer;
				} else {
					s.sortingOrder = playerLayer;
				}
			}
		}
		
		if(Player.Instance().holdingBox){
			if(Player.Instance().transform.Find("Box")){
				Player.Instance().transform.Find("Box/Sprite").gameObject.GetComponent<SpriteRenderer>().sortingOrder = playerBoxLayer;
			} else if(Player.Instance().transform.Find("Box-Invisible")){
				Player.Instance().transform.Find("Box-Invisible/Sprite").gameObject.GetComponent<SpriteRenderer>().sortingOrder = playerBoxLayer;
			}
		}
	}
	
	private static SpriteOrderer instance;
	
	public static SpriteOrderer Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<SpriteOrderer>();
		}
		
		return instance;
	}
}