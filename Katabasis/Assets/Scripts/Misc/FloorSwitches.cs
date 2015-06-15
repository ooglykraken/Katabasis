using UnityEngine;
using System.Collections;

public class FloorSwitches : MonoBehaviour {

	public string function;
	public string argument;
	public GameObject target;
	
	public bool activated = false;
	
	public void Update(){
		bool allActive = true;
		
		foreach(Transform t in transform){
			if(t.tag == "FloorSwitch"){
				if(!t.gameObject.GetComponent<FloorSwitch>().active){
					allActive = false;
					break;
				}
			}
		}
		
		if(allActive){
			ActivateSwitch();
		}
	}
	
	public void ActivateSwitch(){
		if(activated){
			return;
		}
		
		activated = true;
		
		if (target) {
			if (function.Length > 0) {
				if (argument.Length > 0)
					target.SendMessage(function, argument, SendMessageOptions.DontRequireReceiver);
				else
					target.SendMessage(function, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
