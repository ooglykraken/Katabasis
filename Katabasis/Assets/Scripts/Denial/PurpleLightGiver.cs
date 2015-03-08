using UnityEngine;
using System.Collections;

public class PurpleLightGiver : MonoBehaviour {

	
	public void OnCollisionEnter(Collision c)
	{
		if (c.transform.tag == "Player")
		{
			c.gameObject.GetComponent<Player>().hasLens = true;
			TextBox.Instance().UpdateText("You now have the Purple Light on 2. It will allow you to see what really is.");
		}
	}
}
