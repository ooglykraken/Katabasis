using UnityEngine;
using System.Collections;

public class PopupButton : MonoBehaviour {
	
	public void Awake(){
		gameObject.GetComponent<Button>().downTarget = transform.parent.gameObject;
	}
}
