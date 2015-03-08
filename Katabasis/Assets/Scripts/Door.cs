using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	private GameObject openDoor;
	
	public void Awake(){
		// CreateOpenDoor();
	}
	
	public void Destroy(){
		// foreach(Transform t in openDoor.transform){
			// t.renderer.enabled = true;
		// }
		// openDoor.renderer.enabled = true;
		// openDoor.collider.enabled = true;
	
		GetComponent<AudioSource>().Play();
	
		Renderer model = GetComponent<Renderer>();
		Collider collider = GetComponent<Collider>();
		
		model.enabled = false;
		collider.enabled = false;
		
		
	}
	
	private void CreateOpenDoor(){
		// bool isHorizontal;
		Vector3 newSize;
		
		openDoor = Instantiate(Resources.Load("OpenDoor(Puzzle)", typeof(GameObject)) as GameObject) as GameObject;
		
		openDoor.transform.position = this.transform.position;
		openDoor.transform.eulerAngles = this.transform.eulerAngles;
		
		// if(orientation == "vertical"){
			// newSize = new Vector3(transform.localScale.x * .5f, transform.localScale.y, transform.localScale.z);
			// transform.rotation = new Vector3(0
		// } else {
			
		// }
		
		newSize = new Vector3(transform.localScale.x * .5f, transform.localScale.y , transform.localScale.z);
		
		Transform openLeftDoor = openDoor.transform.Find("LeftDoor");
		Transform openRightDoor = openDoor.transform.Find("RightDoor");
		
		openLeftDoor.localScale = newSize;
		openRightDoor.localScale = newSize;
		
		Vector3 halfScale = new Vector3(transform.localScale.x * .5f, 0f, 0f);
		
		openRightDoor.transform.position += new Vector3(transform.localScale.x / 4f, 0f,0f);
		openLeftDoor.transform.position -= new Vector3(transform.localScale.x / 4f, 0f,0f);
		
		openRightDoor.transform.RotateAround(transform.position + halfScale, Vector3.back, 90f);
		openLeftDoor.transform.RotateAround(transform.position - halfScale, Vector3.back, -90f);
		
		openRightDoor.GetComponent<Renderer>().enabled = false;
		openLeftDoor.GetComponent<Renderer>().enabled = false;
	}
	
	public void Reappear(){
		// foreach(Transform t in openDoor.transform){
			// t.renderer.enabled = false;
		// }
	
		Renderer model = GetComponent<Renderer>();
		Collider collider = GetComponent<Collider>();
		
		collider.enabled = true;
		model.enabled = true;
	}
}
