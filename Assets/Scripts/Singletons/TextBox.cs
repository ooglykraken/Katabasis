using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	private int timeToKillText = 150;
	private int timer;
	private int playerModifier;
	
	public GameObject player;
	
	public Transform focus;
	
	public TextMesh txtBox;
	
	public void Awake(){
		txtBox = gameObject.GetComponent<TextMesh>();
		
		txtBox.text = "";
		
		focus = Player.Instance().transform;
		
		playerModifier = 1;
	}
	
	public void FixedUpdate(){
		if(timer == 0){
			txtBox.text = "";
		} else {
			timer--;
		}
		
		// playerModifier = Player.Instance().polarity;
		
		Follow();
	}
	
	private void Follow(){
		transform.position = focus.position + new Vector3(0f, 1.4f * playerModifier, 0f);
	}
	
	public void UpdateText(string s){
		focus = player.transform;
	
		txtBox.text = s;
		
		timer = timeToKillText;
		playerModifier = 1;
		// playerModifier = polarity;
	}
	
	public void UpdateText(Transform t, string s){
		focus = t;
		
		txtBox.text = s;
		
		timer = timeToKillText;
		playerModifier = 1;
	}
	
	private static TextBox instance = null;
	
	public static TextBox Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<TextBox>();
		}
		
		return instance;
	}
}
