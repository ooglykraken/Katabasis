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
			Destroy(lens);
			// GameObject.Find("Player").
		}
	}
}
