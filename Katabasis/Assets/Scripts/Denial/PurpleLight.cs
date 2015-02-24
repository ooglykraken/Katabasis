using UnityEngine;
using System.Collections;

public class PurpleLight : MonoBehaviour {
	
	public void OnTriggerStay (Collider c)
	{
		if (c.name == "PurpleLightFloor")
		{
			c.GetComponent<MeshRenderer>().enabled = true;
			c.tag = "Floor";
		}
	}
	
	public void OnTriggerExit (Collider c)
	{
		if (c.name == "PurpleLightFloor")
		{
			c.GetComponent<MeshRenderer>().enabled = false;
			c.tag = "PurpleLight";
		}
	}
}
