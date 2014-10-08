using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	private XMLNodeList levelsXML;
	
	private XMLNode xml;
	private XMLNode currentLevelXML;
	
	private GameObject player;
	private GameObject gameplay;
	private GameObject walls;
	private GameObject floorTiles;
	
	private int numberOfLevels;
	private int nextLevelToBeLoaded;
	
	public void Awake(){
		xml = XMLParser.Parse((Resources.Load("xmlFLoors", typeof(TextAsset)) as TextAsset).text);
		
		levelsXML = xml.GetNodeList("doc>0>levels>0>level");
		
		numberOfLevels = levelsXML.Count;
		
		gameplay = GameObject.Find("Gameplay");
		player = GameObject.Find("Player");
		walls = GameObject.Find("Walls");
		floorTiles = GameObject.Find("FloorTiles");
	}
	
	public void LoadLevel(){
		nextLevelToBeLoaded = Gameplay.Instance().currentLevel;
		
		if(nextLevelToBeLoaded == numberOfLevels){
			Gameplay.Instance().currentLevel = numberOfLevels--;
		}
		
		currentLevelXML = levelsXML[nextLevelToBeLoaded] as XMLNode;
		
		foreach(XMLNode floorXML in currentLevelXML.GetNodeList("floors>0>floor")){
			GameObject floorTile = Instantiate(Resources.Load("Floor", typeof(GameObject)) as GameObject) as GameObject;
			floorTile.transform.position = new Vector3(float.Parse(floorXML.GetValue("@x")), float.Parse(floorXML.GetValue("@y")), 1f);
			floorTile.transform.localScale = new Vector3(float.Parse(floorXML.GetValue("@sizeX")), float.Parse(floorXML.GetValue("@sizeY")), 1f);
			
			floorTile.transform.parent = floorTiles.transform;
		}
		
		XMLNodeList invisiblesXML = currentLevelXML.GetNodeList("floors>0>invisibleFloor");
		// Debug.Log(invisiblesXML);
		if(invisiblesXML != null)
			foreach(XMLNode floorXML in invisiblesXML){
				GameObject floorTile = Instantiate(Resources.Load("InvisibleFloor", typeof(GameObject)) as GameObject) as GameObject;
				floorTile.transform.position = new Vector3(float.Parse(floorXML.GetValue("@x")), float.Parse(floorXML.GetValue("@y")), 1f);
				floorTile.transform.localScale = new Vector3(float.Parse(floorXML.GetValue("@sizeX")), float.Parse(floorXML.GetValue("@sizeY")), 1f);
				
				floorTile.transform.parent = floorTiles.transform;
			}
		
		foreach(XMLNode wallXML in currentLevelXML.GetNodeList("walls>0>wall")){
			GameObject wall = Instantiate(Resources.Load("Wall", typeof(GameObject)) as GameObject) as GameObject;
			wall.transform.position = new Vector3(float.Parse(wallXML.GetValue("@x")), float.Parse(wallXML.GetValue("@y")), -1f);
			wall.transform.localScale = new Vector3(float.Parse(wallXML.GetValue("@sizeX")), float.Parse(wallXML.GetValue("@sizeY")), 5f);
			
			wall.transform.parent = walls.transform;
		}
		
		XMLNodeList levelObjectsXML = currentLevelXML.GetNodeList("levelObjects>0>object");
		
		XMLNode playerXML = levelObjectsXML[0] as XMLNode;
		XMLNode keyXML = levelObjectsXML[1] as XMLNode;
		XMLNode stairsXML = levelObjectsXML[2] as XMLNode;
		
		GameObject key = Instantiate(Resources.Load("Key", typeof(GameObject)) as GameObject) as GameObject;
		key.transform.position = new Vector3(float.Parse(keyXML.GetValue("@x")), float.Parse(keyXML.GetValue("@y")), float.Parse(keyXML.GetValue("@z")));
		
		GameObject stairs = Instantiate(Resources.Load("Stairs", typeof(GameObject)) as GameObject) as GameObject;
		stairs.transform.position = new Vector3(float.Parse(stairsXML.GetValue("@x")), float.Parse(stairsXML.GetValue("@y")), .4f);
	
		player.transform.position = new Vector3(float.Parse(playerXML.GetValue("@x")),float.Parse(playerXML.GetValue("@y")), 0f);
		
		player.transform.parent = gameplay.transform;
		key.transform.parent = gameplay.transform;
		stairs.transform.parent = gameplay.transform;
		
		List<GameObject> doorList = new List<GameObject>();
		List<GameObject> fanList = new List<GameObject>();
		
		foreach(XMLNode objectXML in levelObjectsXML){
			GameObject o = null;
			
			string type = objectXML.GetValue("@type");
			
			switch(type){
				case "door":
					o = Instantiate(Resources.Load("Door", typeof(GameObject)) as GameObject) as GameObject;
					o.transform.position = new Vector3(float.Parse(objectXML.GetValue("@x")), float.Parse(objectXML.GetValue("@y")), -.5f);
					o.transform.localScale = new Vector3(float.Parse(objectXML.GetValue("@sizeX")), float.Parse(objectXML.GetValue("@sizeY")), 1f);
					o.transform.parent = gameplay.transform;
					doorList.Add(o);
					break;
				case "wallSwitch":
					o = Instantiate(Resources.Load("WallSwitch", typeof(GameObject)) as GameObject) as GameObject;
					o.transform.position = new Vector3(float.Parse(objectXML.GetValue("@x")), float.Parse(objectXML.GetValue("@y")), .5f);
					o.transform.localScale = new Vector3(float.Parse(objectXML.GetValue("@sizeX")), float.Parse(objectXML.GetValue("@sizeY")), 1f);
					o.transform.eulerAngles = new Vector3(0f, 0f, float.Parse(objectXML.GetValue("@rotationZ")));
					o.transform.parent = gameplay.transform;
					break;
				case "floorSwitch":
					o = Instantiate(Resources.Load("FloorSwitch", typeof(GameObject)) as GameObject) as GameObject;
					o.transform.position = new Vector3(float.Parse(objectXML.GetValue("@x")), float.Parse(objectXML.GetValue("@y")), 0f);
					o.transform.localScale = new Vector3(float.Parse(objectXML.GetValue("@sizeX")), float.Parse(objectXML.GetValue("@sizeY")), .7f);
					o.transform.parent = gameplay.transform;
					break;
				case "block":
					o = Instantiate(Resources.Load("Block", typeof(GameObject)) as GameObject) as GameObject;
					o.transform.position = new Vector3(float.Parse(objectXML.GetValue("@x")), float.Parse(objectXML.GetValue("@y")), 0f);
					o.GetComponent<MovableBlock>().startPosition = o.transform.position;
					o.transform.localScale = new Vector3(float.Parse(objectXML.GetValue("@sizeX")), float.Parse(objectXML.GetValue("@sizeY")), 1f);
					o.transform.parent = gameplay.transform;
					break;
				case "fan":
					o = Instantiate(Resources.Load("Fan", typeof(GameObject)) as GameObject) as GameObject;
					o.transform.position = new Vector3(float.Parse(objectXML.GetValue("@x")), float.Parse(objectXML.GetValue("@y")), 0f);
					o.transform.localScale = new Vector3(float.Parse(objectXML.GetValue("@sizeX")), float.Parse(objectXML.GetValue("@sizeY")), 1f);
					o.transform.parent = gameplay.transform;
					fanList.Add(o);
					break;
				default:
					break;
			}
		}
		
		
		WallSwitch w;
		FloorSwitch f;
		switch(Gameplay.Instance().currentLevel){
			case 0:
				
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("WallSwitch")){
					//o.upFunction = "Reapper";
					//o.downFunction = "Destroy";
					w = o.GetComponent<WallSwitch>();
					
					w.target = doorList[1];
				}
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("FloorSwitch")){
					f = o.GetComponent<FloorSwitch>();
					
					f.upTarget = doorList[0];
					f.downTarget = doorList[0];
				}
				break;
			case 1:
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("WallSwitch")){
					w = o.GetComponent<WallSwitch>();
					w.argument = "Key";
					w.target = o;
					w.function = "Drop";
				}
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("FloorSwitch")){
					f = o.GetComponent<FloorSwitch>();
					
					f.upTarget = doorList[0];
					f.downTarget = doorList[0];
				}
				
				break;
			default:
				break;
		}
		
		Gameplay.Instance().currentLevel++;
		fanList.Clear();
		doorList.Clear();
	}
	
	private static Level instance = null;
	
	public static Level Instance(){
		if(instance == null){
			instance = (new GameObject("Level")).AddComponent<Level>();
		}
		return instance;
	}
}
