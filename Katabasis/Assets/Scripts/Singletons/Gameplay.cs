using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	public int finalLevel;
	
	public bool popupOpen;
	public bool isLightOn;
	
	private Player player;
	
	public Transform spawnLocation;
	
	private Color lightsOut;
	private Color lightsOn = Color.white;
	
	public void Awake(){	
		
		// DontDestroyOnLoad(gameObject);
		
		popupOpen = false;
		
		lightsOut = RenderSettings.ambientLight;
		if(Application.loadedLevel == 1){
			LightsOn();
		}
		
		finalLevel = Application.levelCount;
		currentLevel = 0;
	}
	
	public void Start(){
		player = GameObject.Find("Player").GetComponent<Player>();
		spawnLocation = GameObject.Find("SpawnLocation").transform;
	}
	
	public void Update(){
		// currentLevel = Application.loadedLevel - 1;
	
		if(Input.GetKeyDown("escape") && !popupOpen){
			popupOpen = true;
			// Debug.Log("menu time");
			Popup p = Instantiate(Resources.Load("UI/In-Game Menu", typeof(Popup)) as Popup) as Popup; 
			
			// I do this so I don't get the warning about p not being used upon compiling
			p.gameObject.SetActive(true);
		}
		
		if(popupOpen){
			player.GetComponent<Player>().enabled = false;
		} else {
			player.GetComponent<Player>().enabled = true;
		}
		
		// if(!isLightOn){
			// LightsOff();
		// }
		
		player = GameObject.Find("Player").GetComponent<Player>();
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
		// spawnLocation = null;
		// Handle jumping to the next stage.
		if(Application.loadedLevel != Gameplay.Instance().finalLevel){
			FadeToBlack.Instance().FadeIn();
			Application.LoadLevel(Application.loadedLevel + 1);
			LightsOff();
			
		} else {
			FinishGame();
		}
		// spawnLocation = GameObject.Find("SpawnLocation").transform;
	}
	
	public void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void ExitGame(){
		Application.Quit();
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
