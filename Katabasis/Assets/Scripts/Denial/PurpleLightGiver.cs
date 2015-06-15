using UnityEngine;
using System.Collections;

public class PurpleLightGiver : MonoBehaviour {
	
	public GameObject lens;
	
	public void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == "Player" && lens != null)
		{
			Debug.Log(c.gameObject.name);
			
			c.gameObject.GetComponent<Player>().hasLens = true;

			// GameObject.Find("Player").
			transform.Find("Spotlight").GetComponent<Light>().enabled = false;
			TextBox.Instance().UpdateText("Your lantern doesn't deny the truth (press 2)");
		}
	}
}
