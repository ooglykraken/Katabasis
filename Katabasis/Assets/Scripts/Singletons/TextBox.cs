using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	private int timeToKillText = 200;
	private int timer;

	public GameObject player;
	
	public TextMesh txtBox;
	
	public void Awake(){
		txtBox = gameObject.GetComponent<TextMesh>();
		
		txtBox.text = "";
	}
	
	public void Update(){
		if(timer == 0){
			txtBox.text = "";
		} else {
			timer--;
		}
	}
	
	public void FixedUpdate(){
		FollowPlayer();
	}
	
	private void FollowPlayer(){
		transform.position = player.transform.position + new Vector3(0f, 1f, 0f);
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
