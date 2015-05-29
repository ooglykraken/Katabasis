using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour {
	
	private int respawnCooldown = 60;
	public int timeToRespawn;
	
	private GameObject box;

	public void Awake(){
		box = Resources.Load("Box", typeof(GameObject)) as GameObject;
	}
	
	public void Update(){
		if(timeToRespawn > 0){
			timeToRespawn--;
		}
		if(timeToRespawn == 0){
			GameObject g = Instantiate(box as GameObject) as GameObject;
			g.transform.position = transform.position;
			timeToRespawn = respawnCooldown;
		}
	}
}
