using UnityEngine;
using System.Collections;

public class TorriodalWall : MonoBehaviour {

	public string side;

	public void OnTriggerEnter(Collider c)
	{
		if (c.transform.parent.tag == "Player" || c.transform.parent.tag == "Monster")
		{
			Debug.Log(c.transform.parent.tag);
			Transform t = c.transform.parent;
			switch (side)
			{
			case ("Left"):
				Vector3 changedXL = new Vector3(20f, t.transform.position.y, t.transform.position.z);
				t.transform.position = changedXL;
				break;
			case ("Right"):
				Vector3 changedXR = new Vector3(-20f, t.transform.position.y, t.transform.position.z);
				t.transform.position = changedXR;
				break;
			case ("Top"):
				Vector3 changedYT = new Vector3(t.transform.position.x, -20f, t.transform.position.z);
				t.transform.position = changedYT;
				break;
			case ("Bottom"):
				Vector3 changedYB = new Vector3(t.transform.position.x, 20f, t.transform.position.z);
				t.transform.position = changedYB;
				break;
			default:
				break;
			}
		}
	}
}
