using UnityEngine;
using System.Collections;

public class LevelSelectMenu : MonoBehaviour {
	
	public int highestLevel;
	
	public Shader disabledButton;
	
	public void Awake(){
		SaveSystem.Instance().Load();
		highestLevel = SaveSystem.Instance().levelNumber;
		
		// highestLevel = 1;

		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Button")){
			// if(transform.tag == "Button"){
				Debug.Log("BUtton");
				string buttonLevel = g.GetComponent<Button>().downArgument;
				
				int intLevel = 4;
				
				switch(buttonLevel){
					case("Tutorial"):	
						intLevel = 1;
						break;
					case("Denial"):
						intLevel = 2;
						break;
					case("Anger"):
						intLevel = 3;
						break;
					case("Bargaining"):
						intLevel = 4;
						break;
					case("Depression"):
						intLevel = 5;
						break;
					case("Acceptance"):
						intLevel = 6;
						break;
					default:
						break;
				}
				
				if(intLevel > highestLevel){
					g.transform.Find("Model").GetComponent<Renderer>().material.shader = disabledButton;
					g.GetComponent<Collider>().enabled = false;
				}
			// }
		}
	}
	
	public void Load(string level){
		
		Application.LoadLevel(level);
	}
}
