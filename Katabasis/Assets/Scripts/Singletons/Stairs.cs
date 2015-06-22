using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {
	
	// public Material stairsMaterial;
	
	// public Material doorMaterial;
	
	private static float distanceToOpen = 4f;
	
	private GameObject player;
	
	public bool isOpen;
	
	public void Awake(){
		isOpen = false;
		player = GameObject.Find("Player");
	}
	
	public void Update(){
		if(Vector3.Distance(transform.position, player.transform.position) <= distanceToOpen){
			CheckPlayer();
		}
	}
	
	public void OpenDoors(){
		GetComponent<AudioSource>().Play();
			
		transform.Find("SpriteOpen").gameObject.SetActive(true);
		transform.Find("SpriteClosed").gameObject.SetActive(false);

	}
	
	private void CheckPlayer(){
		if(player.GetComponent<Player>().hasFloorKey){
			OpenDoors();
			isOpen = true;
		} else {
			TextBox.Instance().UpdateText("You need a key...");
		}
	}
	
	private static Stairs instance = null;
	
	public static Stairs Instance(){
		if(instance == null){
			instance = GameObject.Find("Stairs").GetComponent<Stairs>();
		}
		
		return instance;
	}
}
