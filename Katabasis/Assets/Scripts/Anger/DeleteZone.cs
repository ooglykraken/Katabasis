using UnityEngine;
using System.Collections;

public class DeleteZone : MonoBehaviour {

	public void OnTriggerEnter(Collider c)
	{
		if (c.transform.parent.tag == "Box")
		{
			Destroy(c.transform.parent.gameObject);
		}
		
		if (c.transform.parent.name == "Player")
		{
			c.gameObject.GetComponent<Player>().Teleport();
		}
	}
}
