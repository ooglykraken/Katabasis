using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using System.Linq;
// using System.Object.String;

public class XMLGenerator : MonoBehaviour {
	
	public bool generateXML;
	
	public void Awake(){
		if(generateXML)
			GenerateXML();
	}
	
	private void GenerateXML(){
		string xmlInformation = "<level number=\"\">\n";
		
		xmlInformation = string.Concat(xmlInformation, "<levelObjects>\n");
		
		GameObject player = GameObject.Find("Player");
		xmlInformation = string.Concat(xmlInformation, "<object type=\"player\" x=\"" + player.transform.position.x +  "\" y=\"" + player.transform.position.y + "\" />\n");
		
		GameObject key = GameObject.Find("Key");
		xmlInformation = string.Concat(xmlInformation, "<object type=\"key\" x=\"" + key.transform.position.x + "\" y=\"" + key.transform.position.y +  "\" z=\"" + key.transform.position.z +  "\"/>\n");
		
		GameObject stairs = GameObject.Find("Stairs");
		xmlInformation = string.Concat(xmlInformation, "<object type=\"stairs\" x=\"" + stairs.transform.position.x + "\" y=\"" + stairs.transform.position.y + "\" />\n");
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Block")){
			xmlInformation = string.Concat(xmlInformation, "<object type=\"block\" x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Fan")){
			xmlInformation = string.Concat(xmlInformation, "<object type=\"fan\" x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("WallSwitch")){
			xmlInformation = string.Concat(xmlInformation, "<object type=\"wallSwitch\"  x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" rotationZ=\"" + o.transform.eulerAngles.z + "\" />\n");
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("FloorSwitch")){
			xmlInformation = string.Concat(xmlInformation, "<object type=\"floorSwitch\"  x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Door")){
			xmlInformation = string.Concat(xmlInformation, "<object type=\"door\" x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		xmlInformation = string.Concat(xmlInformation, "</levelObjects>\n");
		
		xmlInformation = string.Concat(xmlInformation, "<walls>\n");
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Wall")){
			xmlInformation = string.Concat(xmlInformation, "<wall x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("IllusoryWall")){
			xmlInformation = string.Concat(xmlInformation, "<illusoryWall x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
			xmlInformation = string.Concat(xmlInformation, "</walls>\n");
		
		xmlInformation = string.Concat(xmlInformation, "<floors>\n");
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Floor")){
			xmlInformation = string.Concat(xmlInformation, "<floor x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("InvisibleFloor")){
			xmlInformation = string.Concat(xmlInformation, "<invisibleFloor x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		xmlInformation = string.Concat(xmlInformation, "</floors>\n");
		
		xmlInformation = string.Concat(xmlInformation, "</level>\n");
		
		Debug.Log(xmlInformation);
	}
}

