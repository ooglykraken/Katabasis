using UnityEngine;
using System.Collections;

public class InGameUIButton : MonoBehaviour {

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
		downTarget = GameObject.Find("Gameplay");
	}
	
	public void OnMouseOver(){
		
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
}


