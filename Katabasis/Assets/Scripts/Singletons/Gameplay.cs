﻿using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	public int finalLevel;
	
	public bool testing;
	
	public void Awake(){		
		// currentLevel = 0;
		
		if(!testing)
			NextLevel();
	}
	
	public void NextLevel(){
		ClearThisLevel();
	
		Level.Instance().LoadLevel();
		
		//Debug.Log(currentLevel);
	}
	
	private void ClearThisLevel(){
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Wall")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Floor")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("InvisibleFloor")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("StairsDown")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("FloorSwitch")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("WallSwitch")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Block")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Door")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Fan")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Key")){
			Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("IllusoryWall")){
			Destroy(o);
		}
	}
	
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.Find("Gameplay").GetComponent<Gameplay>();
		}
		
		return instance;
	}
}