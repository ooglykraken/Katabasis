using UnityEngine;
using System.Collections;
// using System.Collections.Generic;

public class BoxSpawner : MonoBehaviour {
	
	private int respawnCooldown = 60;
	public int timeToRespawn;
	
	private GameObject box;

	public GameObject[] boxes;
	
	private int boxLimit = 20;
	
	private int boxIndex;
	
	private Vector3 north;
	private Vector3 south;
	
	public void Awake(){
		south = transform.Find("SouthSpawn").position;
		north = transform.Find("NorthSpawn").position;
		
		boxes = new GameObject[boxLimit];
		
		box = Resources.Load("Misc/Box", typeof(GameObject)) as GameObject;
		for(int i = 0; i < boxLimit; i++){
			GameObject b = Instantiate(box as GameObject) as GameObject;
			
			boxes[i] = b;
			
			b.transform.position = new Vector3(-10000f, 5f * i, 0f);
			
			b.transform.parent = transform;
			
			b.transform.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = false;
			b.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
			
			SpriteOrderer.Instance().allSpriteRenderers.Add(b.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		}
		
		boxIndex = 0;
	}
	
	public void Update(){
		if(timeToRespawn > 0){
			timeToRespawn--;
		}
		if(timeToRespawn == 0){
		
			SpawnBox();
			
		}
	}
	
	private void SpawnBox(){
		
		GameObject b = boxes[boxIndex];
		
		if(b == null){
			return;
		}
		
		b.transform.position = north;
		
		if(boxIndex / 2f == Mathf.Round(boxIndex / 2f)){
			b.transform.position = south;
		}
		
		b.GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		timeToRespawn = respawnCooldown;
		
		b.transform.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = true;
		b.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = true;
		
		boxIndex++;
		
		if(boxIndex == boxLimit){
			boxIndex = 0;
		}
	}
}
