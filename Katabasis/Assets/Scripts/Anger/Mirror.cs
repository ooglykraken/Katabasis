using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {

	private Light light;
	private bool isFiring;
	void Awake()
	{
		light = gameObject.transform.FindChild("Light").GetComponent<Light>();
	}

	public IEnumerator ReflectRaycast()
	{
		light.enabled = true;
		isFiring = true;
		RaycastHit hit;
		
		if(Physics.Raycast(transform.position, transform.up, out hit))
		{
			if (hit.transform.tag == "Mirror")
			{
				StartCoroutine(hit.transform.gameObject.GetComponent<Mirror>().ReflectRaycast());
			}
			
			if (hit.transform.name == "BlastDoor")
			{
				Destroy(hit.transform.gameObject);
			}
		}
		
		yield return new WaitForSeconds(1f);
		light.enabled = false;
		isFiring = false;
	}
	
	public void RotateMirror()
	{
		transform.Rotate (0, 0, 45f);
	}
}
