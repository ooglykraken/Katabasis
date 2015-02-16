using UnityEngine;
using System.Collections;

public class FloorSwitch : MonoBehaviour {

	public GameObject downTarget;
	public GameObject upTarget;
	
	public string downFunction;
	public string upFunction;
	public string downArgument;
	public string upArgument;
	
	private bool active;
	
	public void OnTriggerStay(Collider c){
		if(active){
			return;
		}
	
		transform.Find("Plate").localPosition = new Vector3(transform.Find("Plate").localPosition.x, transform.Find("Plate").localPosition.y, -.01f);
		
		if(c.transform.parent.tag == "Block" || c.transform.parent.tag == "Player" || c.transform.parent.tag == "SmokeEnemy"){
			
			active = true;
			
			audio.Play();
			
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
		
		active = false;
		
		transform.Find("Plate").localPosition = new Vector3(transform.Find("Plate").localPosition.x, transform.Find("Plate").localPosition.y, -1f);
		
		if(c.transform.parent.tag == "Block" || c.transform.parent.tag == "Player" || c.transform.parent.tag == "SmokeEnemy"){
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
