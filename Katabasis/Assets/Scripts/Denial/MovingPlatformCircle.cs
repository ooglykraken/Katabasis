using UnityEngine;
using System.Collections;

public class MovingPlatformCircle : MonoBehaviour {
	public float speed;
	
	public void FixedUpdate()
	{
		this.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
	}
}