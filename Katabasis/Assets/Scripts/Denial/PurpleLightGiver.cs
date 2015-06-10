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
			TextBox.Instance().UpdateText("You now have the Purple Light on 2. It will allow you to see what really is.");

			TextBox.Instance().UpdateText("You now have the Purple Light on 2. It will allow you to see what really is.");

		}
	}
}
