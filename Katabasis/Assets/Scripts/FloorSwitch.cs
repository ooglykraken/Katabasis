using UnityEngine;
using System.Collections;

public class FloorSwitch : MonoBehaviour {

	public GameObject downTarget;
	public GameObject upTarget;
	
	public string downFunction;
	public string upFunction;
	public string downArgument;
	public string upArgument;
	
	public void OnTriggerStay(Collider c){
		
		// Debug.Log(c.transform.parent.tag);
		
		transform.position = new Vector3(transform.position.x, transform.position.y, .5f);
		
		if(c.transform.parent.tag == "Block" || c.transform.parent.tag == "Player"){
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
	
	public void OnTriggerExit(Collider c){
		
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		
		if(c.transform.parent.tag == "Block" || c.transform.parent.tag == "Player"){
			if (upTarget) {
				if (upFunction.Length > 0) {
					if (upArgument.Length > 0)
						upTarget.SendMessage(upFunction, upArgument, SendMessageOptions.DontRequireReceiver);
					else
						upTarget.SendMessage(upFunction, upArgument, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
