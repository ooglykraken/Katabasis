using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

	private float fanStrength = .08f;

	public void OnTriggerStay(Collider c){
		if(c.transform.parent.tag == "Player" || c.transform.parent.tag == "Block" ||  c.transform.parent.tag == "Key"){
			Transform t = c.transform.parent;
			t.position += new Vector3(0f, fanStrength, 0f);
		}
	}	
}
