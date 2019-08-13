using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {

	private Light lightBeam;
	
	private bool isFiring;
	
	void Awake()
	{
		lightBeam = gameObject.transform.Find("LightBeam").GetComponent<Light>();
	}

	public void ReflectRaycast()
	{
		if(isFiring){
			return;
		}
		
		lightBeam.enabled = true;
		isFiring = true;
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
		
		lightBeam.enabled = false;
		isFiring = false;
	}
	
	public void RotateMirror()
	{
		transform.Rotate (0, 0, 45f);
	}
}
