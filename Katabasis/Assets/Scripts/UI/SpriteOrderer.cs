using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteOrderer : MonoBehaviour {
	
	private static int backLayer = 0;
	private static int playerLayer = 1;
	private static int frontLayer = 2;
	
	private GameObject player;
	
	public List<SpriteRenderer> allSpriteRenderers = new List<SpriteRenderer>();
	
	public void Awake(){
		player = GameObject.Find("Player");
	
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
			if(s.gameObject.transform.parent.tag == "FloorSwitch" || s.gameObject.transform.parent.tag == "WallSwitch"){
				s.sortingOrder = backLayer;
			} else {
		
				if(player.transform.position.y > s.transform.parent.position.y){
					s.sortingOrder = frontLayer;
				} else if(player.transform.position.y < s.transform.parent.position.y){
					s.sortingOrder = backLayer;
				} else {
					s.sortingOrder = playerLayer;
				}
			}
		}
	}
	
	private static SpriteOrderer instance;
	
	public static SpriteOrderer Instance(){
		if(instance == null){
			instance = GameObject.Find("SpriteOrderer").GetComponent<SpriteOrderer>();
		}
		
		return instance;
	}
}