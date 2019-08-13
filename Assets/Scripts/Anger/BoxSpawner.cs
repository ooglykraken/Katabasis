using UnityEngine;
using System.Collections;
// using System.Collections.Generic;

public class BoxSpawner : MonoBehaviour {
	
	private int respawnCooldown = 200;
	public int timeToRespawn;
	
	private GameObject box;

	public GameObject[] boxes;
	
	private int boxLimit = 6;
	
	private int boxIndex;
	
	private int numberOfSpawners;
	
	public int id;
	
	private Vector3 north;
	private Vector3 south;
	private Vector3 east;
	
	public void Awake(){
		south = transform.Find("SouthSpawn").position;
		north = transform.Find("NorthSpawn").position;
		east = transform.Find("EastSpawn").position;
		
		boxes = new GameObject[boxLimit];
		
		box = Resources.Load("Misc/Box", typeof(GameObject)) as GameObject;
		for(int i = 0; i < boxLimit; i++){
			GameObject b = Instantiate(box as GameObject) as GameObject;
			
			b.gameObject.GetComponent<Box>().startScale = b.transform.localScale;
			
			boxes[i] = b;
			
			b.transform.position = new Vector3(-10000f, 5f * i, 0f);
			
			b.transform.parent = transform;
			
			b.transform.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = false;
			b.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
			
			b.name = b.name.Replace("(Clone)", "");
			
			// b.GetComponent<Box>().isDoomed = false;
			
			SpriteOrderer.Instance().allSpriteRenderers.Add(b.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		}
		
		boxIndex = 0;
		id = 0;
		numberOfSpawners = 3;
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
		
		id = Mathf.RoundToInt(Mathf.Repeat(boxIndex, numberOfSpawners));
		
		switch(id){
			case 0:
				b.transform.position = north;
				b.GetComponent<Box>().Resize();
				break;
			case 1:
				b.transform.position = south;
				b.GetComponent<Box>().Resize();
				break;
			case 2:
				b.transform.position = east;
				b.GetComponent<Box>().Resize();
				break;
			default:
				break;
		}
		
		b.GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		timeToRespawn = respawnCooldown;
		
		b.transform.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = true;
		b.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = true;
		
		// b.GetComponent<Box>().isDoomed = true;
		// b.GetComponent<Box>().lifespan = 450;
		
		boxIndex++;
		
		if(boxIndex == boxLimit){
			boxIndex = 0;
		}
	}
}
