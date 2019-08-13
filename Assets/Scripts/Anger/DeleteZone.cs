using UnityEngine;
using System.Collections;

public class DeleteZone : MonoBehaviour {

	public void OnTriggerEnter(Collider c)
	{
		// Debug.Log(c.transform.parent.name);
	
		if (c.transform.parent.tag == "Box")
		{
			// Debug.Log("Invisible!");
			c.transform.parent.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
			c.transform.parent.Find("Sprite").gameObject.GetComponent<Renderer>().enabled = false;
			
			c.transform.parent.position = new Vector3(-10000f, 0f, 0f);
		}
		
		// if (c.transform.parent.name == "Player")
		// {
			// c.transform.parent.gameObject.GetComponent<Player>().Teleport();
		// }
	}
}
