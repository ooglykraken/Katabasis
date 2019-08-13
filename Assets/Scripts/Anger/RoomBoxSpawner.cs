using UnityEngine;
using System.Collections;

public class RoomBoxSpawner : MonoBehaviour {
	
	private GameObject box;

	public GameObject[] boxes;
	
	private Bounds bounds;
	
	private int itemWidth = 4;
	private int itemHeight = 5;
	// private int itemsToSpawn = 16;
	
	public float roomWidth;
	public float roomHeight;
	public float itemsPerRow;
	public float rowLimit;
	public float numberOfRows;
	
	public void Awake(){
		bounds = gameObject.GetComponent<Collider>().bounds;
		
		roomWidth = bounds.size.x * 2;
		roomHeight = bounds.size.y * 2;
	}
	
	public void Start(){
		
		// boxes = new GameObject[itemsToSpawn];
		 
		// int limit = boxLimit;
		
		
		itemsPerRow = Mathf.Floor(roomWidth / itemWidth);
		rowLimit = Mathf.Floor(roomHeight / itemHeight);
		// numberOfRows = Mathf.Floor(itemsToSpawn/itemsPerRow);
		
		Vector3 startPoint = new Vector3(-roomWidth - 5f, -roomHeight - 2f, 0f) / 100;
		
		box = Resources.Load("Misc/Box", typeof(GameObject)) as GameObject;
		for(int i = 0; i < rowLimit; i++){
			for(int j = 0; j < itemsPerRow; j++){
				// Debug.Log(i + " , " + j);
				float xOffset = (itemWidth / 10);
				float yOffset = (itemHeight / 10);
				
				GameObject b = Instantiate(box as GameObject) as GameObject;
				
				b.name = b.name.Replace("(Clone)", "");
				
				// boxes.Add(b);
				
				b.transform.parent = transform;
				
				b.transform.localPosition = new Vector3(startPoint.x + (j * .1f), startPoint.y + (i * .12f), 0f);
				
				b.transform.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = true;
				b.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = true;
				
				SpriteOrderer.Instance().allSpriteRenderers.Add(b.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
				
				
			}
		}
		
	}
	
	// private void SpawnBox(){
		
		// GameObject b = boxes[boxIndex];
		
		// if(b == null){
			// return;
		// }
		
		// b.transform.position = north;
		
		// if(boxIndex / 2f == Mathf.Round(boxIndex / 2f)){
			// b.transform.position = south;
		// }
		
		// b.GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		// timeToRespawn = respawnCooldown;
		
		// b.transform.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = true;
		// b.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = true;
		
		// b.GetComqponent<Box>().isDoomed = true;
		// b.GetComponent<Box>().lifespan = 450;
		
		// boxIndex++;
		
		// if(boxIndex == boxLimit){
			// boxIndex = 0;
		// }
	// }
}
