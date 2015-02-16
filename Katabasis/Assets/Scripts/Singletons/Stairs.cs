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
		
		audio.Play();
		
		transform.Find("Model").gameObject.GetComponent<Renderer>().material = stairsMaterial;
		
		transform.localScale = new Vector3(3.5f, 2f, 1f);
		
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
