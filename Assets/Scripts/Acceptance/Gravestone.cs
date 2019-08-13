using UnityEngine;
using System.Collections;

public class Gravestone : MonoBehaviour {
	
	public string engraving;
	
	public void ReadStone(){
		Player.Instance().Speak(engraving);
	}
}
