using UnityEngine;
using System.Collections;

public class InterstitialButton : MonoBehaviour {

	public GameObject downTarget;
	
	public string downArgument;
	public string downFunction;
	
	public void Update(){
		// Debug.Log("Hovering");
	
		if(Input.anyKeyDown){
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
