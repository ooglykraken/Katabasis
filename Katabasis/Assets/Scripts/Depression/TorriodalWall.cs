using UnityEngine;
using System.Collections;

public class TorriodalWall : MonoBehaviour {

	public string side;

	public void OnTriggerEnter(Collider c)
	{
		if (c.transform.parent.tag == "Player")
		{
			Transform playerTransform = c.transform.parent;
			switch (side)
			{
			case ("Left"):
				Vector3 changedXL = new Vector3(28.5f, playerTransform.transform.position.y, playerTransform.transform.position.z);
				playerTransform.transform.position = changedXL;
				break;
			case ("Right"):
				Vector3 changedXR = new Vector3(-28.5f, playerTransform.transform.position.y, playerTransform.transform.position.z);
				playerTransform.transform.position = changedXR;
				break;
			case ("Top"):
				Vector3 changedYT = new Vector3(playerTransform.transform.position.x, -28.5f, playerTransform.transform.position.z);
				playerTransform.transform.position = changedYT;
				break;
			case ("Bottom"):
				Vector3 changedYB = new Vector3(playerTransform.transform.position.x, 28.5f, playerTransform.transform.position.z);
				playerTransform.transform.position = changedYB;
				break;
			default:
				break;
			}
		}
	}
}
