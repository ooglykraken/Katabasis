using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject startButton;
	public GameObject levelButton;
	public GameObject exitButton;
	
	public void Awake(){
		if(!GameObject.Find("SaveSystem(Clone)")){
			GameObject saveSystem = Instantiate(Resources.Load("Misc/SaveSystem", typeof(GameObject)) as GameObject) as GameObject;
		}
	}
	
	private void OnMouseDown(string argument){
		switch(argument){
			case "StartGame":
				StartGame();
				break;
			case "LevelSelect":
				LevelSelect();
				break;
			case "ExitGame":
				Exit();
				break;
			default:
				break;
		}
	}
	
	private void StartGame(){
		Application.LoadLevel("Tutorial");
	}
	
	private void LevelSelect(){
		Application.LoadLevel("LevelSelect");
	}
	
	private void Exit(){
		Application.Quit();
	}
	
	private void Settings(){
		GameObject p = Instantiate(Resources.Load("UI/Popup", typeof(GameObject)) as GameObject) as GameObject; 
		p.transform.parent = GameObject.Find("MainMenu").transform;
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("UIText")){
			if(g.transform.parent.parent.name != "Popup(Clone)"){
				
				g.GetComponent<Renderer>().enabled = false;
			}
		}
	}
	
	private static MainMenu instance = null;
	
	public static MainMenu Instance(){
		if (instance == null)
			instance = GameObject.FindObjectOfType<MainMenu>();
			
		return instance;
	}
}
