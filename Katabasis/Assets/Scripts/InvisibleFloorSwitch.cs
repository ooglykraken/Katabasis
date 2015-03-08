using UnityEngine;
using System.Collections;

public class InvisibleFloorSwitch : MonoBehaviour {

	public GameObject target;
	
	public string function;
	public string argument;
	
	public void OnTriggerEnter(Collider c){
		if (target) {
			if (function.Length > 0) {
				if (argument.Length > 0)
					target.SendMessage(function, argument, SendMessageOptions.DontRequireReceiver);
				else
					target.SendMessage(function, SendMessageOptions.DontRequireReceiver);
			}
		}
		
		if(Gameplay.Instance().currentLevel == 0 && Gameplay.Instance().isLightOn){
			Gameplay.Instance().LightsOff();
		}
		
		Destroy(gameObject);
	}
}
