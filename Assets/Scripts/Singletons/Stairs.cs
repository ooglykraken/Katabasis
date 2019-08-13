using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {
	
	// public Material stairsMaterial;
	
	// public Material doorMaterial;
	
	private static float distanceToOpen = 2f;
	
	private GameObject player;
	
	public bool isOpen;
	
	public void Awake(){
		isOpen = false;
		player = Player.Instance().gameObject;
	}
	
	public void Update(){
		if(Vector3.Distance(transform.position, player.transform.position) <= distanceToOpen){
			if(!isOpen){
				CheckPlayer();
			}
		}
	}
	
	public void OpenDoors(){
		gameObject.GetComponent<AudioSource>().Play();
			
		transform.Find("SpriteOpen").gameObject.SetActive(true);
		transform.Find("SpriteClosed").gameObject.SetActive(false);

	}
	
	private void CheckPlayer(){
		if(player.GetComponent<Player>().hasFloorKey){
			OpenDoors();
			isOpen = true;
		} else {
			Player.Instance().Speak("I need a key...");
		}
	}
	
	private static Stairs instance = null;
	
	public static Stairs Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<Stairs>();
		}
		
		return instance;
	}
}
