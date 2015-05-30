using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {
	
	public Vector3 direction; 
	
	public float speed;

	public bool isHorizontal;
	public bool isReverse; // negative on the cartesian, given that y and x are positive
	
	public void Update(){
			if(isHorizontal){
				direction = transform.right;
			} else {
				direction = transform.up;
			}
			
			if(isReverse){
				direction *= -1f;
			}
	}
	
	public void OnCollisionStay(Collision c)
	{
		if (c.gameObject.tag == "Box")
		{
			
			

			c.gameObject.GetComponent<Rigidbody>().AddForce(direction * speed * Time.deltaTime);
		}	
	}
}
