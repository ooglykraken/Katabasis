using UnityEngine;
using System.Collections;

public class WallSwitch : MonoBehaviour {
	
	public Sprite leverUp;
	public Sprite leverDown;
	
	public GameObject target;
	
	private GameObject tutorialBox;
	
	// private static float activationDistance = 1.5f;
	
	public string function;
	public string argument;
	
	public bool isFlipped;
	
	private int tutorialBoxDropDelay = 20;
	
	private bool isDropped;
	// each switch should get one piece (a floor prefab) and turns the mesh renderer on.
	// public GameObject bridgePiece;
	
	public void Update(){
		// if(!isFlipped){
			// if(PlayerDistance() <= activationDistance){
				// ActivateSwitch();
			// }
		// }
		if(isFlipped && tutorialBoxDropDelay > 0){
			tutorialBoxDropDelay--;
		}
		
		if(tutorialBoxDropDelay <= 0 && isFlipped && !isDropped && Application.loadedLevel == 1){
			tutorialBox.GetComponent<Box>().boxDrop.Play();
			isDropped = true;
		}
	}
	
	public void OnTriggerEnter(Collider c){
		// if (c.name != "Lens")
		// {
			// if(c.transform.parent.tag == "Player"){
				// ActivateSwitch();
				// TextBox.Instance().UpdateText("You hear a click nearby...");
			// }
		// }
	}
	
	public void ActivateSwitch(){
		if(isFlipped){
			return;
		}
		
		GetComponent<AudioSource>().Play();
		
		isFlipped = true;
		
		transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = leverDown;
		
		if (target) {
			if (function.Length > 0) {
				if (argument.Length > 0)
					target.SendMessage(function, argument, SendMessageOptions.DontRequireReceiver);
				else
					target.SendMessage(function, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	public void Drop(string s){
		GameObject droppedObject = GameObject.FindGameObjectWithTag(s);
		Debug.Log(droppedObject);
		droppedObject.transform.position = new Vector3(droppedObject.transform.position.x, droppedObject.transform.position.y, 0f);
	}
	
	public void Create(string s){
		Debug.Log(s);
		GameObject newThing = Instantiate(Resources.Load("misc/" + s, typeof(GameObject)) as GameObject) as GameObject;
		switch(Gameplay.Instance().currentLevel){
			case 1:
				newThing.transform.position = new Vector3(transform.position.x - 2f, transform.position.y - 4f, -2f);
				tutorialBox = newThing;
				// newThing.GetComponent<Box>().boxDrop.Play();
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
		
		if(newThing.transform.Find("Sprite")){
			SpriteOrderer.Instance().allSpriteRenderers.Add(newThing.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		}
		
		newThing.name = newThing.name.Replace("(Clone)", "");
	}
}
