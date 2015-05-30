using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	private int laserDuration = 20;
	public int laserTimer;
	
	private int laserRange = 8;
	
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
	
		Vector3 ray = transform.position;
		if (Physics.CapsuleCast(ray , ray, 1, transform.forward, out hit, distance)) {
			if(hit.transform.tag == "Box" || hit.transform.tag == "Breakable"){
				// Debug.Log("IT'S A BEAM!");
				// laserTimer = laserDuration;
				// beamMesh.enabled = true;
				// GetComponent<AudioSource>().Play();
				Destroy(hit.transform.gameObject);
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
