using UnityEngine;
using System.Collections;

public class TellControls : MonoBehaviour {

	public GameObject textBox;

	void Start () 
	{
		textBox.GetComponent<TextBox>().UpdateText("Use w,a,s, and d to move. Use e to activate things.");
	}
	
}
