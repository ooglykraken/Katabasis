using UnityEngine;
using System.Collections;

public class RedLightGiver : MonoBehaviour {

	public GameObject textBox;

	public void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == "Player")
		{
			c.gameObject.GetComponent<Player>().hasLaser = true;
			textBox.GetComponent<TextBox>().UpdateText("You now have the red light on 3. Use it to break obstacles or remove enemies...");
		}
	}
}
