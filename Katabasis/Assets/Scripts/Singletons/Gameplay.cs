using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	public int finalLevel;
	
	// public bool testing;
	// public bool lightsOn;
	
	public bool popupOpen;
	
	private Color lightsOut;
	private Color lightsOn = Color.white;
	
	public void Awake(){	
		
		DontDestroyOnLoad(gameObject);
		
		popupOpen = false;
		
		lightsOut = RenderSettings.ambientLight;
		LightsOn();
		// lightsOn = true;
	
		// currentLevel = 0;
		
		// if(!testing)
			// NextLevel();
	}
	
	public void Update(){
		if(Input.GetKeyDown("escape") && !popupOpen){
			popupOpen = true;
			// Debug.Log("menu time");
			Popup p = Instantiate(Resources.Load("UI/Popup", typeof(Popup)) as Popup) as Popup; 
		}
		
		if(popupOpen){
			GameObject.Find("Player").GetComponent<Player>().enabled = false;
		} else {
			GameObject.Find("Player").GetComponent<Player>().enabled = true;
		}
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
			instance = (new GameObject("Gameplay")).AddComponent<Gameplay>();
		}
		
		return instance;
	}
}
