using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private void OnMouseDown(string argument){
		switch(argument){
			case "StartGame":
				StartGame();
				break;
			case "Settings":
				Settings();
				break;
			default:
				break;
		}
	}
	
	private void StartGame(){
		Application.LoadLevel("Gameplay");
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
}
