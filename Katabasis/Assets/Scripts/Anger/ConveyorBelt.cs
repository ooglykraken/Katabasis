using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {
	
	public Vector3 direction; 
	
	public float speed;

	public bool isHorizontal;
	public bool isReverse; // negative on the cartesian, given that y and x are positive
	
	public void Awake(){
 
	}
	
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
		// Debug.Log(c.transform.parent.name);
		if (c.transform.tag == "Box" || c.transform.tag == "Player")
		{
			Rigidbody rigidbody = c.gameObject.GetComponent<Rigidbody>();
		
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, (direction * speed), Time.deltaTime);
		}	
	}
}