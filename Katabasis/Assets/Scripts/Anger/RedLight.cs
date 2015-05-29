using UnityEngine;
using System.Collections;

public class RedLight : MonoBehaviour {

<<<<<<< HEAD
=======
// <<<<<<< HEAD
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
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
		
<<<<<<< HEAD
		yield return new WaitForSeconds(.5f);
		isFiring = false;
=======
	}
	
// =======
	public void FixedUpdate()
	{	
		CheckForBreakableWalls();
	}

	public void CheckForBreakableWalls()
	{
		RaycastHit hit;
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
		
	}
<<<<<<< HEAD
=======
// >>>>>>> b96716b3173aead13a198c842107b05758f21160
>>>>>>> 7c08b8ae9bbbaf64abfc57918a0420738bf6fcab
}
