using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using System.Linq;
// using System.Object.String;

public class XMLGenerator : MonoBehaviour {
	
	// List<string> xmlInformationXML = new Lis	t<string>();
	
	public void Awake(){
		string xmlInformation = "";
		
		xmlInformation = string.Concat(xmlInformation, "<levelObjects>\n");
		
		GameObject player = GameObject.Find("Player");
		xmlInformation = string.Concat(xmlInformation, "<object type=\"player\" x=\"" + player.transform.position.x +  "\" y=\"" + player.transform.position.x + "\" />\n");
		
		GameObject key = GameObject.Find("Key");
		xmlInformation = string.Concat(xmlInformation, "<object type=\"key\" x=\"" + key.transform.position.x + "\" y=\"" + key.transform.position.x +  "\" />\n");
		
		GameObject stairs = GameObject.Find("Stairs");
		xmlInformation = string.Concat(xmlInformation, "<object type=\"stairs\" x=\"" + stairs.transform.position.x + "\" y=\"" + stairs.transform.position.x + "\" />\n");
		
		xmlInformation = string.Concat(xmlInformation, "</levelObjects>\n");
		
		xmlInformation = string.Concat(xmlInformation, "<walls>\n");
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Wall")){
			xmlInformation = string.Concat(xmlInformation, "<wall x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
			xmlInformation = string.Concat(xmlInformation, "</walls>\n");
		
		xmlInformation = string.Concat(xmlInformation, "<floors>\n");
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Floor")){
			xmlInformation = string.Concat(xmlInformation, "<floor x=\"" + o.transform.position.x + "\" y=\"" + o.transform.position.y + "\" sizeX=\"" +  o.transform.lossyScale.x + "\" sizeY=\"" + o.transform.lossyScale.y + "\" />\n");
		}
		
		xmlInformation = string.Concat(xmlInformation, "</floors>");
		
		Debug.Log(xmlInformation);
	}
}
