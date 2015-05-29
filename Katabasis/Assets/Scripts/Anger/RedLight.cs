using UnityEngine;
using System.Collections;

public class RedLight : MonoBehaviour {

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
			if(hit.transform.tag == "DepressionMonster")
			{
				hit.transform.gameObject.GetComponent<DepMonsterAI>().Respawn();
			}
		}
		
		yield return new WaitForSeconds(.5f);
		
		redLight.enabled = false;
		
		yield return new WaitForSeconds(.5f);
		isFiring = false;
		
	}
}
