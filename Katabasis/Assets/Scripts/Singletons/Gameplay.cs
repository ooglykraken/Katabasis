using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	public int finalLevel;
	
	// public bool testing;
	// public bool lightsOn;
	
	private Color lightsOut;
	private Color lightsOn = Color.white;
	
	public void Awake(){	
	
		lightsOut = RenderSettings.ambientLight;
		LightsOn();
		// lightsOn = true;
	
		// currentLevel = 0;
		
		// if(!testing)
			// NextLevel();
	}
	
	public void LightsOff(){
		RenderSettings.ambientLight = lightsOut;
	}
	
	public void LightsOn(){
		RenderSettings.ambientLight = lightsOn;
	}
	
	public void NextLevel(){
		// ClearThisLevel();
	
		// Level.Instance().LoadLevel();
		
		//Debug.Log(currentLevel);
	}
	
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.Find("Gameplay").GetComponent<Gameplay>();
		}
		
		return instance;
	}
}
