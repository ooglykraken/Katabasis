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
<<<<<<< HEAD
			// GameObject.Find("Player").
=======
			TextBox.Instance().UpdateText("You now have the Purple Light on 2. It will allow you to see what really is.");
>>>>>>> b96716b3173aead13a198c842107b05758f21160
		}
	}
}
