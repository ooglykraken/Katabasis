using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour {

	private int timeToKillText = 250;
	private int timer;

	public GameObject player;
	
	public Transform focus;
	
	public TextMesh txtBox;
	
	public void Awake(){
		txtBox = gameObject.GetComponent<TextMesh>();
		
		txtBox.text = "";
		
		focus = GameObject.Find("Player").transform;
	}
	
	public void FixedUpdate(){
		if(timer == 0){
			txtBox.text = "";
		} else {
			timer--;
		}
		
		Follow();
	}
	
	private void Follow(){
		transform.position = focus.position + new Vector3(0f, 1.4f, 0f);
	}
	
	public void UpdateText(string s){
		focus = player.transform;
	
		txtBox.text = s;
		
		timer = timeToKillText;
	}
	
	public void UpdateText(Transform t, string s){
		focus = t;
		
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
