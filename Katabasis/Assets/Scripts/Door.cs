using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void Destroy(){
		Renderer model = GetComponent<Renderer>();
		Collider collider = GetComponent<Collider>();
		
		model.enabled = false;
		collider.enabled = false;
	}
	
	public void Reappear(){
		Renderer model = GetComponent<Renderer>();
		Collider collider = GetComponent<Collider>();
		
		collider.enabled = true;
		model.enabled = true;
	}
}
