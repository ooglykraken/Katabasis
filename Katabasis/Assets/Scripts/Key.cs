using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public void FixedUpdate(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * 6f);
	}
}
