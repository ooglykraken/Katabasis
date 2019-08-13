using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int currentLevel;
	private int finalLevel;
	
	public bool popupOpen;
	public bool isLightOn;
	
	private Player player;
	
	public Transform spawnLocation;
	
	private Color lightsOut;
	private Color lightsOn = Color.white;
	
	public void Awake(){	
		
		// DontDestroyOnLoad(gameObject);
		
		popupOpen = false;
		
		finalLevel = Application.levelCount;
		currentLevel = Application.loadedLevel;
		
		lightsOut = RenderSettings.ambientLight;
		if(currentLevel == 1){
			LightsOn();
		}
	}
	
	public void Start(){
		player = Player.Instance();
		spawnLocation = GameObject.Find("SpawnLocation").transform;
		
		StartInterstitial(currentLevel);
	}
	
	public void Update(){
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
			
			if(SaveSystem.Instance() != null){
				SaveSystem.Instance().Save(Application.loadedLevel + 1);
			}
			
			Application.LoadLevel(Application.loadedLevel + 1);
			
			LightsOff();
			
		} else {
			FinishGame();
		}
		// spawnLocation = GameObject.Find("SpawnLocation").transform;
	}
	
	private void StartInterstitial(int level){
		GameObject g = null;
		// Debug.Log(level);
		switch(level){
			case 1:
				// gameObject.GetComponent<AudioSource>().Stop();
				g = Resources.Load("Interstitials/Tutorial", typeof(GameObject)) as GameObject;
				break;
			case 2:
				g = Resources.Load("Interstitials/Denial", typeof(GameObject)) as GameObject;
				break;
			case 3:
				g = Resources.Load("Interstitials/Anger", typeof(GameObject)) as GameObject;
				break;
			case 4:
				g = Resources.Load("Interstitials/Bargaining", typeof(GameObject)) as GameObject;
				break;
			case 5:
				g = Resources.Load("Interstitials/Depression", typeof(GameObject)) as GameObject;
				break;
			case 6:
				// g = Resources.Load("Interstitials/Acceptance", typeof(GameObject)) as GameObject;
				break;
			default:
				Debug.Log("The interstitial loader has failed to load the proper level");
				break;
		}
		if(g != null){
			GameObject interstitial = Instantiate(g) as GameObject;
			popupOpen = true;
		} else {
			FadeToBlack.Instance().FadeOut();
		}
	}
	
	public void CloseInterstitial(GameObject interstitial){
		// FadeToBlack.Instance().FadeIn();
		// yield WaitForSeconds(1);
		// for(int i = 0; i < 100000; i++){
			// Debug.Log(i);
		// }
		Destroy(interstitial);
		popupOpen = false;
		// yield WaitForSeconds(.25);
		// FadeToBlack.Instance().FadeOut();
	}
	
	public void ReturnToMainMenu(){
		Application.LoadLevel(0);
	}
	
	public void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void ExitGame(){
		Application.Quit();
	}
	
	public void FinishGame(){
		Application.LoadLevel("Credits");
	}
	
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			// instance = (new GameObject("Gameplay")).AddComponent<Gameplay>();
			instance = GameObject.FindObjectOfType<Gameplay>();
		}
		
		return instance;
	}
}
