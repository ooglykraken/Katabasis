using UnityEngine;
using System.Collections;

public class LightGiver : MonoBehaviour {

	// public GameObject textBox;
	
	private static float activationDistance = 1.5f;
	
	private bool isUsed;
	
	public void Awake(){
		isUsed = false;
	}
	
	public void Update(){
		
		if(!isUsed && PlayerDistance() <= activationDistance){
			Player player = GameObject.Find("Player").GetComponent<Player>();
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
		gameObject.SetActive(false);
	}
	
	public void ActivateLens(Player p)
	{
		p.hasLens = true;
		transform.Find("Spotlight").GetComponent<Light>().enabled = false;
		TextBox.Instance().UpdateText("Your lantern isn't denied the truth (press 2)");
		isUsed = true;
	}
	
	public void ActivateLaser(Player p)
	{
		p.hasLaser = true;
		transform.Find("Spotlight").GetComponent<Light>().enabled = false;
		TextBox.Instance().UpdateText("Your lantern now channels your anger (press 3)");
		isUsed = true;
	}
	
	private float PlayerDistance(){
		if(gameObject.name == "LanternGiver"){
			return Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
		}
	
		return Vector3.Distance(transform.Find("Collider").position, GameObject.Find("Player").transform.position);
	}
}
