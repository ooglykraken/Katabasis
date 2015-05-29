using UnityEngine;
using System.Collections;

public class MAGLaser : MonoBehaviour {

	// private Light light;
	private bool isFiring;
	
	void Awake()
	{
		// light = gameObject.transform.FindChild("Light").GetComponent<Light>();
	}
	
	public IEnumerator Fire()
	{
		Debug.Log("Mag firing");
		GetComponent<Light>().enabled = true;
		isFiring = true;
		RaycastHit hit;
		
		if(Physics.Raycast(transform.position, transform.up, out hit))
		{
			if (hit.transform.gameObject.tag == "Mirror")
			{
				StartCoroutine(hit.transform.gameObject.GetComponent<Mirror>().ReflectRaycast());
			}
		}
		
		yield return new WaitForSeconds(1f);
		GetComponent<Light>().enabled = false;
		isFiring = false;
	}
}
