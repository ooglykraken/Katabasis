using UnityEngine;
using System.Collections;

public class MAGLaser : MonoBehaviour {

	public void Fire()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.up, out hit))
		{
			if (hit.transform.gameObject.tag == "Mirror")
			{
				hit.transform.gameObject.GetComponent<Mirror>().ReflectRaycast();
			}
		}
	}
}
