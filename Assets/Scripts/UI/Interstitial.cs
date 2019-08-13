using UnityEngine;
using System.Collections;

public class Interstitial : MonoBehaviour {

	public GameObject currentPanel;
	public int panelNumber;
	public int numberOfPanels;

	public void Awake(){
		
		currentPanel = transform.Find("Frame/Panel_1").gameObject;
		panelNumber = 1;
		
		numberOfPanels = transform.Find("Frame").childCount;
		
		Player.Instance().enabled = false;
		// FadeToBlack.Instance().FadeOut();
	}
	
	public void NextPanel(){
		if(currentPanel.name == "Panel_" + numberOfPanels){
			ExitInterstitial();
		} else {
			currentPanel.SetActive(false);
			panelNumber++;
			currentPanel = transform.Find("Frame/Panel_" + panelNumber).gameObject;
			currentPanel.SetActive(true);
		}
	}
	
	public void PreviousPanel(){
		if(currentPanel.name == "Panel_1"){
		} else {
			currentPanel.SetActive(false);
			panelNumber--;
			currentPanel = transform.Find("Frame/Panel_" + panelNumber).gameObject;
			currentPanel.SetActive(true);
		}
	}
	
	public void ExitInterstitial(){
		if(gameObject.GetComponent<AudioSource>()){
			Gameplay.Instance().gameObject.GetComponent<AudioSource>().Play();
		}
	
		Player.Instance().enabled = true;
		Gameplay.Instance().CloseInterstitial(this.gameObject);
		// Debug.Log("Closing interstitial");
		// Destroy(this.gameObject);
		// FadeToBlack.Instance().FadeOut();
	}
}
