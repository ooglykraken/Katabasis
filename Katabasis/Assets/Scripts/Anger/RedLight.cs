using UnityEngine;
using System.Collections;

public class RedLight : MonoBehaviour {

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
}
