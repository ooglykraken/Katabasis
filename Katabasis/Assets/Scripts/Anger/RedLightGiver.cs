using UnityEngine;
using System.Collections;

public class RedLightGiver : MonoBehaviour {

	public GameObject textBox;

	public void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == "Player")
		{
			c.gameObject.GetComponent<Player>().hasLaser = true;
			transform.Find("Spotlight").GetComponent<Light>().enabled = false;
			textBox.GetComponent<TextBox>().UpdateText("Your lantern now channel your anger (press 3)");
		}
	}
}
