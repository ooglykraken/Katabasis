using UnityEngine;
using System.Collections;

public class WallSwitch : MonoBehaviour {

	public GameObject target;
	
	public string function;
	public string argument;
	
	bool isFlipped;
	
	// each switch should get one piece (a floor prefab) and turns the mesh renderer on.
	public GameObject bridgePiece;
	
	public void OnTriggerEnter(Collider c){
		if (c.name != "Lens")
		{
			if(c.transform.parent.tag == "Player"){
				ActivateSwitch();
				// TextBox.Instance().UpdateText("You hear a click nearby...");
			}
		}
	}
	
	private void ActivateSwitch(){
		if(isFlipped){
			return;
		}
		
		GetComponent<AudioSource>().Play();
		
		isFlipped = true;
	
		Transform handle = transform.Find("Handle").GetComponent<Transform>();
		handle.eulerAngles	= new Vector3(transform.eulerAngles.x, 600f, transform.eulerAngles.z);
		
		if (target) {
			if (function.Length > 0) {
				if (argument.Length > 0)
					target.SendMessage(function, argument, SendMessageOptions.DontRequireReceiver);
				else
					target.SendMessage(function, SendMessageOptions.DontRequireReceiver);
			}
		}
		
		if (bridgePiece != null)
		{
			Debug.Log("Floor is active now!");
			bridgePiece.SetActive (true);
		}
	}
	
	public void Drop(string s){
		GameObject droppedObject = GameObject.FindGameObjectWithTag(s);
		Debug.Log(droppedObject);
		droppedObject.transform.position = new Vector3(droppedObject.transform.position.x, droppedObject.transform.position.y, 0f);
	}
	
	public void Create(string s){
		Debug.Log(s);
		GameObject newThing = Instantiate(Resources.Load(s, typeof(GameObject)) as GameObject) as GameObject;
		switch(Gameplay.Instance().currentLevel){
			case 0:
				newThing.transform.position = new Vector3(-6f, 43f, -8f);
				break;
			case 2:
				newThing.transform.parent = GameObject.Find("FloorTiles").transform;
				newThing.transform.localScale = new Vector3(3.5f, 3f, 1f);
				newThing.transform.position = new Vector3(4.25f, 6f, 1f);
				break;
			default:
				newThing.transform.position = new Vector3(-6f, 43f, -1f);
				break;
		}
	}
}
