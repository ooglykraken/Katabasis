using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	private int timeToKillText = 150;
	private int timer;

	public GameObject player;
	
	public TextMesh txtBox;
	
	public void Awake(){
		txtBox = gameObject.GetComponent<TextMesh>();
		
		txtBox.text = "";
	}
	
	public void FixedUpdate(){
		if(timer == 0){
			txtBox.text = "";
		} else {
			timer--;
		}
		
		FollowPlayer();
	}
	
	private void FollowPlayer(){
		transform.position = player.transform.position + new Vector3(0f, 1.4f, 0f);
	}
	
	public void UpdateText(string s){
		txtBox.text = s;
		
		timer = timeToKillText;
	}
	
	private static TextBox instance = null;
	
	public static TextBox Instance(){
		if(instance == null){
			instance = GameObject.Find("TextBox").GetComponent<TextBox>();
		}
		
		return instance;
	}
}
