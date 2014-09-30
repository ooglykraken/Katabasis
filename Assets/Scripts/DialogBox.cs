using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour {

	private int lifeTime = 1000;
	
	private GameObject dialogBox;

	public void Awake(){
		dialogBox = gameObject;
		transform.position = Vector3.zero;
	}
	
	public void Update(){
		if(lifeTime <= 0){
			Destroy(gameObject);
			Destroy(this);
		} else {
			lifeTime--;
		}
	}
	
	public void LateUpdate(){
		if(transform.eulerAngles.z != 0f){
			transform.eulerAngles = Vector3.zero;
		}
	}
}
