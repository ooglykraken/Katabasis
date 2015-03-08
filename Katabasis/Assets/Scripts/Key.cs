using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public void FixedUpdate(){
		GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 6f);
	}
}
