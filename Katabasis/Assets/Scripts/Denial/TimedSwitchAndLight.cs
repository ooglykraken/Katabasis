using UnityEngine;
using System.Collections;

public class TimedSwitchAndLight : MonoBehaviour {

	public GameObject downTarget;
	public GameObject upTarget;
	
	public string downFunction;
	public string upFunction;
	public string downArgument;
	public string upArgument;
	
	private bool active;
	private Light switchLight;
	public float timer;
	
	public void Awake()
	{
		switchLight = GetComponentInParent<Light>();
	}
	
	public void OnTriggerStay(Collider c){
		if(active){
			return;
		}
		
		if (c.name != "Lens")
		{
			
			transform.Find("Plate").localPosition = new Vector3(transform.Find("Plate").localPosition.x, transform.Find("Plate").localPosition.y, -.01f);
			switchLight.color = Color.green;
			
			if(c.transform.parent.tag == "Box" || c.transform.parent.tag == "Player" || c.transform.parent.tag == "SmokeEnemy"){
				
				active = true;
				
				GetComponent<AudioSource>().Play();
				
				StartCoroutine(Reset ());
				
				
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
	
	public IEnumerator Reset(){
	
		yield return new WaitForSeconds(timer);
		active = false;
		
		transform.Find("Plate").localPosition = new Vector3(transform.Find("Plate").localPosition.x, transform.Find("Plate").localPosition.y, -1f);
		switchLight.color = Color.red;
		
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
