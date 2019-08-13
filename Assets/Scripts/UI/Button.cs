using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject downTarget;
	
	public string downArgument;
	public string downFunction;
	
	//Special addendum for Katabasis
	
	private GameObject startButton;
	private GameObject levelButton;
	private GameObject exitButton;
	
	private ParticleSystem startParticles;
	private ParticleSystem levelParticles;
	private ParticleSystem exitParticles;
	
	public void Awake(){
		// if(transform.gameObject.name == "BtnStart" || transform.gameObject.name == "BtnExitGame" || transform.gameObject.name == "BtnLevelSelect"){
			// downTarget = GameObject.Find("Gameplay");
		// }
	}
	
	public void Start(){
		if(transform.gameObject.name == "BtnStart" || transform.gameObject.name == "BtnExitGame" || transform.gameObject.name == "BtnLevelSelect"){
			startButton = MainMenu.Instance().startButton;
			levelButton = MainMenu.Instance().levelButton;
			exitButton = MainMenu.Instance().exitButton;
			
			startParticles = startButton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
			levelParticles = levelButton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
			exitParticles = exitButton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
			
			startParticles.Clear();
			exitParticles.Clear();
			levelParticles.Clear();
			
			startParticles.Pause();
			exitParticles.Pause();
			levelParticles.Pause();
		}
	}
	
	public void OnMouseOver(){
		// Debug.Log(gameObject.name);
		
		switch(gameObject.name){
			case "BtnStart":
				Debug.Log("Main Menu Katabasis Button");
				startButton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
				break;
			case "BtnExitGame":
				Debug.Log("Main Menu Katabasis Button");
				exitButton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
				break;
			case "BtnLevelSelect":
				Debug.Log("Main Menu Katabasis Button");
				levelButton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
				break;
			default:
				break;
		}

		
		if(Input.GetMouseButtonDown(0)){
			if (downTarget) {
				if (downFunction.Length > 0) {
					if (downArgument.Length > 0)
						downTarget.SendMessage(downFunction, downArgument, SendMessageOptions.DontRequireReceiver);
					else
						downTarget.SendMessage(downFunction, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	public void OnMouseExit(){
		Debug.Log("Mouse Exit");
		
		switch(gameObject.name){
			case "BtnStart":
				startParticles.Clear();
				startParticles.Pause();
				break;
			case "BtnExitGame":
				exitParticles.Clear();
				exitParticles.Pause();
				break;
			case "BtnLevelSelect":
				levelParticles.Clear();
				levelParticles.Pause();
				break;
			default:
				break;
		}
	}
}


