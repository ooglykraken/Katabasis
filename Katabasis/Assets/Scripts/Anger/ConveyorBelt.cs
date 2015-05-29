using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public float speed;

	public void OnCollisionStay(Collision c)
	{
		if (c.gameObject.tag == "Box"|| c.gameObject.tag == "Player")
		{
			c.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.up * speed * Time.deltaTime);
		}
	}
}
