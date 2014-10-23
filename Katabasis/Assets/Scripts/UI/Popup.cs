using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	private void SetPlayStyle(string s){
		Debug.Log(s);
		
		if(s == "Pointer"){
			Settings.Instance().isUsingDpad = false;
		} else if(s == "D-Pad"){
			Settings.Instance().isUsingDpad = true;
		}
		
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("UIText")){
			if(g.transform.parent.parent.name != "Popup(Clone)"){
				
				g.GetComponent<Renderer>().enabled = true;
			}
		}
		
		Destroy(gameObject);
	}
}
