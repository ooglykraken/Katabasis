using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject downTarget;
	
	public string downArgument;
	public string downFunction;
	
	//Special addendum for Katabasis
	public void Awake(){
		if(transform.parent.gameObject.name == "BtnRestart" || transform.parent.gameObject.name == "BtnExitGame"){
			downTarget = GameObject.Find("Gameplay");
		}
	}
	
	public void OnMouseOver(){
		Debug.Log("Hovering");
	
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
