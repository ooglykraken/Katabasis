using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

	private float fanStrength = .08f;

	public void OnTriggerStay(Collider c){
		Debug.Log(c.transform.parent.rigidbody.velocity);
	
		if(c.transform.parent.tag == "Player" || c.transform.parent.tag == "Block"){
			Transform t = c.transform.parent;
			t.rigidbody.position = new Vector3(t.rigidbody.position.x, t.rigidbody.position.y + fanStrength, 0f);
		}
	}
	
	// public void OnTriggerExit(Collider c){
		// if(c.transform.parent.tag == "Player" || c.transform.parent.tag == "Block"){
			// Transform t = c.transform.parent;
			// t.rigidbody.position = new Vector3(t.rigidbody.position.x, t.rigidbody.position.y - fanStrength, 0f);
		// }
	// }
	
	// private void StartBlowing(Transform t){
		// Debug.Log(t.rigidbody.velocity.y);
		// t.rigidbody.velocity += new Vector3(0f, -3f, 0f);
	// }
	
	// private void StopBlowing(Transform t){
		// t.rigidbody.velocity += new Vector3(0f, 3f, 0f);
	// }
}
