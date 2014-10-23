using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	public bool isUsingDpad;
	
	public void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	
	private static Settings instance = null;
	
	public static Settings Instance(){
		if(instance == null){
			instance = (new GameObject("Settings")).AddComponent<Settings>();
		}
		
		return instance;
	}
}
