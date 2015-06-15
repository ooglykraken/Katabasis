using UnityEngine;
using System.Collections;

public class CreditRoll : MonoBehaviour {

	// private GameObject background;
	
	private float length;

	public Transform mainCamera;
	
	public GameObject lastItem;
	
	private float timeToStart = 200f;

	public void Awake(){
		// background = transform.Find ("Background").gameObject;
		//length = background.gameObject.GetComponent<Renderer>().bounds.extents.y;
		
		//mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}
	
	public void LateUpdate(){ 
		if(timeToStart > 0){
			timeToStart--;
			Debug.Log (timeToStart);
		}
		
		if(timeToStart <= 0 && mainCamera.transform.position.y >= lastItem.transform.position.y - 5f){
			mainCamera.transform.position += new Vector3(0f, -.09f, 0f);
		}
	}
}
