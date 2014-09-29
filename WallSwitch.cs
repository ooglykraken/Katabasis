using UnityEngine;
using System.Collections;

public class WallSwitch : MonoBehaviour {

	public GameObject target;
	
	public string function;
	public string argument;
	
	public void OnTriggerEnter(Collider c){
		
		Transform handle = transform.Find("Handle").GetComponent<Transform>();
		handle.eulerAngles	= new Vector3(transform.eulerAngles.x, 600f, transform.eulerAngles.z);
		
		Debug.Log(handle.eulerAngles);
		
		if(c.transform.parent.tag == "Player"){
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
	
	// public void OnTriggerExit(Collider c){
		
		// transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		
	// }
}
