using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {

	public void ReflectRaycast()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.up, out hit))
		{
			if (hit.transform.tag == "Mirror")
			{
				hit.transform.gameObject.GetComponent<Mirror>().ReflectRaycast();
			}
			
			if (hit.transform.name == "BlastDoor")
			{
				Destroy(hit.transform.gameObject);
			}
		}
	}
	
	public void RotateMirror()
	{
		transform.Rotate (0, 0, 45f);
	}
}
