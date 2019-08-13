using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour {
	
	public string platform;
	
	public int levelNumber;
	private int prefLevel;
	
	private string savePath;
	
	public void Awake(){
		DontDestroyOnLoad(gameObject);
		
		switch(Application.platform){
			case RuntimePlatform.WindowsPlayer:
				savePath = Application.persistentDataPath + "/saveData.dat";
				platform = "windows";
				break;
			case RuntimePlatform.OSXPlayer:
				savePath = Application.persistentDataPath + "/saveData.dat";
				platform = "osx";
				break;
			case RuntimePlatform.WindowsWebPlayer:
				// savePath = "%APPDATA%\Unity\WebPlayerPrefs";
				platform = "windowsweb";
				break;
			case RuntimePlatform.OSXWebPlayer:
				// savePath =  "~/Library/Preferences/Unity/WebPlayerPrefs";
				platform = "osxweb";
				break;
			default:
				savePath = Application.persistentDataPath + "/saveData.dat";
				break;
		}
	}
	
	public void OnEnable(){
		// Load();
	}
	
	public void OnDisable(){
		// Save();
	}
	
	public void Save(int level){
		
		if(platform == "osx" || platform == "windows"){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(savePath);
			
			SaveData save = new SaveData();
			
			if(level > save.levelCompleted){
				save.levelCompleted = level;
			}
			
			bf.Serialize(file, save);
			file.Close();
		} else if(platform == "osxweb" || platform == "windowsweb"){
			PlayerPrefs.SetInt("level", level);
			prefLevel = level;
		}
	}
	
	public void Load(){
		if(platform == "osx" || platform == "windows"){
			if(File.Exists(savePath)){
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(savePath, FileMode.Open);
				SaveData save = (SaveData)bf.Deserialize(file);
				file.Close();
				
				levelNumber = save.levelCompleted;
			}
		} else if(platform == "osxweb" || platform == "windowsweb"){
			prefLevel = PlayerPrefs.GetInt("level");
			levelNumber = prefLevel;
		}
	}
	
	private static SaveSystem instance;
	
	public static SaveSystem Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<SaveSystem>();
		}
		
		return instance;
	}
}

[Serializable]
public class SaveData{
	public int levelCompleted;
}