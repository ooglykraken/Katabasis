using UnityEngine;
using System.Collections;

public class CreditRoll : MonoBehaviour {

	// private GameObject background;
	
	private float length;
	
	private bool finished;
	
	public Transform mainCamera;
	
	public GameObject lastItem;
	
	private float timeToStart = 50f;
	private float waitTilMainMenu = 100f;

	public void Awake(){
		finished = false;
	}
	
	public void LateUpdate(){ 
		if(finished){
			waitTilMainMenu--;
			if(waitTilMainMenu <= 0f){
				Application.LoadLevel("MainMenu");
			}
		}
	
		if(timeToStart > 0){
			timeToStart--;
			Debug.Log (timeToStart);
		}else {
			finished = true;
		}
		if(timeToStart <= 0 && mainCamera.transform.position.y >= lastItem.transform.position.y){
			mainCamera.transform.position += new Vector3(0f, -.05f, 0f);
			finished = false;
		}
		
	}	
}
