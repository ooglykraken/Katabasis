using UnityEngine;
using System.Collections;

public class MAGLaser : MonoBehaviour {

	// private Light light;
	private bool isFiring;
	
	public void Awake()
	{
		// isFiring = false;
		// light = gameObject.transform.FindChild("Light").GetComponent<Light>();
	}
	
	public void Activate(){
		if(!isFiring){
			Fire();
		}
	}
	
	private void Fire(){
		Debug.Log("Mag firing");
		GetComponent<Light>().enabled = true;
		isFiring = true;
		RaycastHit hit;
		
		if(Physics.Raycast(transform.position, transform.up, out hit))
		{
			if (hit.transform.gameObject.tag == "Mirror")
			{
				hit.transform.gameObject.GetComponent<Mirror>().ReflectRaycast();
			}
		}
		
		GetComponent<Light>().enabled = false;
		isFiring = false;
	}
	
	private static MAGLaser instance;
	
	public static MAGLaser Instance(){
		if(instance == null){
			instance = GameObject.Find("MAGLaser").GetComponent<MAGLaser>();
		}
		
		return instance;
	}
}
