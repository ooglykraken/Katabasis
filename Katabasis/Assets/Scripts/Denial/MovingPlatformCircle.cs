using UnityEngine;
using System.Collections;

public class MovingPlatformCircle : MonoBehaviour {
	public float speed;
	
	public void FixedUpdate()
	{
		this.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
		gameObject.transform.Find("PurpleLightFloor").Rotate(-Vector3.forward, speed * Time.deltaTime);
		if (gameObject.transform.Find ("Player"))
		{
			gameObject.transform.Find ("Player").Rotate (-Vector3.forward, speed * Time.deltaTime);
		}
	}
}