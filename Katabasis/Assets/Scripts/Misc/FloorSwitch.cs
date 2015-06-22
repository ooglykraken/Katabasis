using UnityEngine;
using System.Collections;

public class FloorSwitch : MonoBehaviour {

	public GameObject downTarget;
	public GameObject upTarget;
	public GameObject activatingObject;
	
	public string downFunction;
	public string upFunction;
	public string downArgument;
	public string upArgument;
	
	public bool active;
	
	public void Update(){
		
	}
	
	public void OnTriggerStay(Collider c){
		if(active){
			return;
		}
		
		if (c.isTrigger == false)
		{
			
			// Debug.Log(c.name);
			
			activatingObject = c.transform.gameObject;
			
			transform.Find("Plate").localPosition = new Vector3(transform.Find("Plate").localPosition.x, transform.Find("Plate").localPosition.y, -.01f);
			
			if((c.transform.parent.tag == "Box" && c.transform.name != "Box-Fake") || c.transform.parent.tag == "Player" || c.transform.parent.tag == "SmokeEnemy"){
				
				active = true;
				
				GetComponent<AudioSource>().Play();
				
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
	
	public void OnTriggerExit(Collider c){
		if(c.transform.gameObject != activatingObject){
			return;
		}
		active = false;
		
		if (c.transform.name != "Lens")
		{
			transform.Find("Plate").localPosition = new Vector3(transform.Find("Plate").localPosition.x, transform.Find("Plate").localPosition.y, -1f);
			
			if((c.transform.parent.tag == "Box" && c.transform.name != "Box-Fake") || c.transform.parent.tag == "Player" || c.transform.parent.tag == "SmokeEnemy"){
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
}
