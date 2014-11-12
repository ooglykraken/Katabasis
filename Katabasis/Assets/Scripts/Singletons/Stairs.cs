using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {
	
	public Material stairsMaterial;
	
	public Material doorMaterial;
	
	public void OnTriggerEnter(Collider c){
		if(GameObject.Find("Player").GetComponent<Player>().hasFloorKey){
			OpenDoors();
		} else {
			TextBox.Instance().UpdateText("You need a key...");
		}
	}
	
	public void OpenDoors(){
		Debug.Log("OPEN");
		transform.Find("Model").gameObject.GetComponent<Renderer>().material = stairsMaterial;
		
		gameObject.GetComponent<Collider>().enabled = false;
	}
	
	private static Stairs instance = null;
	
	public static Stairs Instance(){
		if(instance == null){
			instance = GameObject.Find("Stairs").GetComponent<Stairs>();
		}
		
		return instance;
	}
}
