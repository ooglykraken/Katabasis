using UnityEngine;
using System.Collections;

public class MovingPlatformCircle : MonoBehaviour {
	public float speed;
	
	public void FixedUpdate()
	{
		this.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
		if(transform.Find("Player") != null){
			transform.Find("Player").Rotate(-Vector3.forward, speed * Time.deltaTime);
		}
	}
}