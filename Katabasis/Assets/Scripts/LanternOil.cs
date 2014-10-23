using UnityEngine;
using System.Collections;

public class LanternOil : MonoBehaviour {

	private float playerStartingLightRange;
	private float playerStartingLightIntensity;


	// Use this for initialization
	void Start () 
	{
		playerStartingLightRange = GameObject.Find ("Lantern").GetComponent<Light> ().range;
		playerStartingLightIntensity = GameObject.Find ("Lantern").GetComponent<Light> ().intensity;
	}
	
	// Update is called once per frame
	public void OnTriggerEnter (Collider c) 
	{
		if (c.transform.parent.tag == "Player") 
		{
			GameObject.Destroy(gameObject);
			GameObject.Find ("Lantern").GetComponent<Light> ().range = playerStartingLightRange;
			GameObject.Find ("Lantern").GetComponent<Light> ().intensity = playerStartingLightIntensity;
		}
	}
}
