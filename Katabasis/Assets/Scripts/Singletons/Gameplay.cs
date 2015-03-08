using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	public int finalLevel;
	
	public bool popupOpen;
	public bool isLightOn;
	
	private Color lightsOut;
	private Color lightsOn = Color.white;
	
	public void Awake(){	
		
		DontDestroyOnLoad(gameObject);
		
		popupOpen = false;
		
		lightsOut = RenderSettings.ambientLight;
		LightsOn();
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
		
		if(!isLightOn){
			LightsOff();
		}
	}
	
	public void LightsOff(){
		RenderSettings.ambientLight = lightsOut;
		isLightOn = false;
	}
	
	public void LightsOn(){
		RenderSettings.ambientLight = lightsOn;
		isLightOn = true;
	}
	
	public void NextLevel(){
		
		// Handle jumping to the next stage.
		if(Application.loadedLevel != Gameplay.Instance().finalLevel){
			Application.LoadLevel(Application.loadedLevel + 1);
			LightsOff();
		} else {
			FinishGame();
		}
	}
	
	public void FinishGame(){
	}
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			// instance = (new GameObject("Gameplay")).AddComponent<Gameplay>();
			instance = GameObject.Find("Gameplay").GetComponent<Gameplay>();
		}
		
		return instance;
	}
}
