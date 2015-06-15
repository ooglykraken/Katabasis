using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	private int laserDuration = 20;
	public int laserTimer;
	
	private int laserRange = 10;
	
	public MeshRenderer beamMesh;
	
	public void Awake(){
		beamMesh = transform.Find("Beam").gameObject.GetComponent<MeshRenderer>();
		beamMesh.enabled = false;
	}
	
	public void Update(){
		
		if(laserTimer > 0 && beamMesh.enabled)
			laserTimer--;
		
		if(laserTimer == 0){
			beamMesh.enabled = false;
		}
		if(Input.GetKeyDown("space") && (laserTimer == laserDuration/2 || laserTimer == 0)){
			
			Fire();
			EnableBeam();
		}
	}
	
	public void Fire(){
		int distance = laserRange;
		RaycastHit hit;
		
		Vector3 positionModifier = Vector3.zero;
		switch(transform.parent.gameObject.GetComponent<Player>().playerDirection){
			case(0):
				positionModifier = -transform.up;
				break;
			case(1):
				positionModifier = -transform.right;
				break;
			case(2):
				positionModifier = transform.up;
				break;
			case(3):
				positionModifier = transform.right;
				break;
			default:
				break;
		}
	
		Vector3 ray = transform.parent.position;
		if (Physics.CapsuleCast(ray, ray, 2f, transform.forward, out hit, distance)) {
			if(hit.transform.tag == "Box"){
				// Debug.Log("IT'S A BEAM!");
				// laserTimer = laserDuration;
				// beamMesh.enabled = true;
				// GetComponent<AudioSource>().Play();
				Debug.Log(hit.transform.tag);
				hit.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().enabled = false;
				hit.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
			} else if(hit.transform.tag == "Enemy"){
			
				Debug.Log("Enemy Hit");
				hit.transform.gameObject.SetActive(false);
			} else if(hit.transform.tag == "Breakable"){
				GameObject g = hit.transform.gameObject;
				g.GetComponent<Collider>().enabled = false;
				g.transform.Find("Model").gameObject.GetComponent<MeshRenderer>().enabled = false;
			} else {
			}
		}
	}
	
	public void EnableBeam(){
			
			beamMesh.enabled = true;
			GetComponent<AudioSource>().Play();
			Debug.Log("IT'S A BEAM!");
			laserTimer = laserDuration;
	}
}
