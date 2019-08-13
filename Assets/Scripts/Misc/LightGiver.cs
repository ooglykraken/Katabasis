using UnityEngine;
using System.Collections;

public class LightGiver : MonoBehaviour {

	// public GameObject textBox;
	
	private static float activationDistance = 2f;
	
	private bool isUsed;
	
	public void Awake(){
		isUsed = false;
	}
	
	public void Update(){
		
		if(!isUsed && PlayerDistance() <= activationDistance){
			Player player = Player.Instance();
			switch(gameObject.name){
				case("LanternGiver"):
					ActivateLantern(player);
					break;
				case("LensGiver"):
					ActivateLens(player);
					break;
				case("LaserGiver"):
					ActivateLaser(player);
					break;
				default:
					Debug.Log(gameObject.name + " should not have this script");
					break;
			}
		}
	}
	
	public void ActivateLantern(Player p)
	{
		p.hasLantern = true;
		p.transform.Find("Lantern").gameObject.SetActive(true);
		Debug.Log("Stuff");
		gameObject.SetActive(false);
	}
	
	public void ActivateLens(Player p)
	{
		p.hasLens = true;
		transform.Find("Spotlight").GetComponent<Light>().enabled = false;
		Player.Instance().Speak("Press <- to reveal hidden objects for some time");
		isUsed = true;
	}
	
	public void ActivateLaser(Player p)
	{
		p.hasLaser = true;
		transform.Find("Spotlight").GetComponent<Light>().enabled = false;
		Player.Instance().Speak("Press -> to fire the laser");
		isUsed = true;
	}
	
	private float PlayerDistance(){
		if(gameObject.name == "LanternGiver"){
			return Vector3.Distance(transform.position, Player.Instance().transform.position);
		}
	
		return Vector3.Distance(transform.Find("Collider").position, Player.Instance().transform.position);
	}
}
