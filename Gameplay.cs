using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	public int finalLevel = 4;
	
	public void Awake(){		
		currentLevel = 0;
	
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
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("StairsDown")){
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
