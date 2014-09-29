using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void Destroy(){
		Renderer model = transform.Find("Model").GetComponent<Renderer>();
		Collider collider = transform.Find("Collider").GetComponent<Collider>();
		
		model.enabled = false;
		collider.enabled = false;
	}
	
	public void Reappear(){
		Renderer model = transform.Find("Model").GetComponent<Renderer>();
		Collider collider = transform.Find("Collider").GetComponent<Collider>();
		
		collider.enabled = true;
		model.enabled = true;
	}
}
