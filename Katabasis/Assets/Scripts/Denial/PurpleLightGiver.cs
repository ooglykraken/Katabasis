using UnityEngine;
using System.Collections;

public class PurpleLightGiver : MonoBehaviour {
	
	public void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == "Player")
		{
			c.gameObject.GetComponent<Player>().hasLens = true;
		}
	}
}
