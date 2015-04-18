using UnityEngine;
using System.Collections;

public class RedLight : MonoBehaviour {

// <<<<<<< HEAD
	public Light redLight;
	public bool isFiring;
	
	public void Update()
	{
		if (Input.GetKeyUp("space"))
		{
			StartCoroutine(Fire ());
		}
	}
	
	public IEnumerator Fire()
	{
		redLight.enabled = true;
		isFiring = true;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, gameObject.transform.forward, out hit, 5f))
		{
			if(hit.transform.tag == "Breakable")
			{
				Destroy(hit.transform.gameObject);
			}
			if (hit.transform.tag == "Box")
			{
				Destroy(hit.transform.gameObject);
			}
		}
		
		yield return new WaitForSeconds(1f);
		
		redLight.enabled = false;
		isFiring = false;
		
	}
	
// =======
	public void FixedUpdate()
	{	
		CheckForBreakableWalls();
	}

	public void CheckForBreakableWalls()
	{
		RaycastHit hit;
		
		Vector3 ray  = new Vector3(transform.position.x, transform.position.y, transform.lossyScale.z * .5f);
		if (Physics.Raycast(ray, transform.up, out hit))
		{
			Debug.Log (hit.transform.gameObject.name);
			if (hit.transform.tag == "Breakable")
			{
				//hit.transform.gameObject.GetComponent<BreakableWall>().Break();
			}
			
			if (hit.transform.tag == "Puzzle Piece")
			{
				//do something
			}
		}
	}
// >>>>>>> b96716b3173aead13a198c842107b05758f21160
}
