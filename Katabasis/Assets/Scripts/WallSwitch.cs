using UnityEngine;
using System.Collections;

public class WallSwitch : MonoBehaviour {

	public GameObject target;
	
	public string function;
	public string argument;
	
	public void OnTriggerEnter(Collider c){
		
		Transform handle = transform.Find("Handle").GetComponent<Transform>();
		handle.eulerAngles	= new Vector3(transform.eulerAngles.x, 600f, transform.eulerAngles.z);
		
		// Debug.Log(handle.eulerAngles);
		
		if(c.transform.parent.tag == "Player"){
			if (target) {
				if (function.Length > 0) {
					if (argument.Length > 0)
						target.SendMessage(function, argument, SendMessageOptions.DontRequireReceiver);
					else
						target.SendMessage(function, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	public void Drop(string s){
		GameObject droppedObject = GameObject.FindGameObjectWithTag(s);
		Debug.Log(droppedObject);
		droppedObject.transform.position = new Vector3(droppedObject.transform.position.x, droppedObject.transform.position.y, 0f);
	}
	
	public void Create(string s){
		GameObject newThing = Instantiate(Resources.Load(s, typeof(GameObject)) as GameObject) as GameObject;
		switch(Gameplay.Instance().currentLevel){
			case 2:
				newThing.transform.parent = GameObject.Find("FloorTiles").transform;
				newThing.transform.localScale = new Vector3(3.5f, 3f, 1f);
				newThing.transform.position = new Vector3(4.25f, 6f, 1f);
				break;
			default:
				break;
		}
	}
}
