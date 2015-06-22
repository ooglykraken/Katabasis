using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	private int laserDuration = 20;
	public int laserTimer;
	
	private int laserRange = 15;
	
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
	
		Vector3 ray = new Vector3(transform.parent.Find("Collider").position.x, transform.parent.Find("Collider").position.y, transform.parent.position.z);
		if (Physics.SphereCast(ray - positionModifier, 1f, transform.forward, out hit, distance)) {
			Debug.Log(hit.transform.tag);
			if(hit.transform.tag == "Box"){
				// Debug.Log("IT'S A BEAM!");
				// laserTimer = laserDuration;
				// beamMesh.enabled = true;
				// GetComponent<AudioSource>().Play();
				Debug.Log(hit.transform.tag);
				hit.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().enabled = false;
				hit.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
			} else if(hit.transform.tag == "Monster"){
			
				Debug.Log("Enemy Hit");
				hit.transform.gameObject.GetComponent<DepMonsterAI>().Kill();
			} else if(hit.transform.parent.tag == "Breakable"){
				GameObject g = hit.transform.parent.gameObject;
				g.transform.Find("Collider").gameObject.GetComponent<Collider>().enabled = false;
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
