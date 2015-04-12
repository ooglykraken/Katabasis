using UnityEngine;
using System.Collections;

public class RedLightGiver : MonoBehaviour {

	public void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == "Player")
		{
			c.gameObject.GetComponent<Player>().hasLaser = true;
		}
	}
}
