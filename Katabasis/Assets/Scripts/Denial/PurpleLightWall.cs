using UnityEngine;
using System.Collections;

public class PurpleLightWall : MonoBehaviour {

	private GameObject thisWall;
	private BoxCollider theBoxCollider;
	private MeshRenderer modelRenderer;
	private GameObject player;
	private Vector3 changedPosition = new Vector3(0, 0, -1.5f);
	
	public void Awake()
	{
		thisWall = gameObject.transform.parent.gameObject;
		theBoxCollider = thisWall.GetComponent<BoxCollider>();
		modelRenderer = thisWall.GetComponentInChildren<MeshRenderer>();
	}
	
	public void OnTriggerStay(Collider c)
	{
		if (c.name != "Lens")
		{
			if (c.transform.parent.tag == "Player")
			{
				player = c.transform.parent.gameObject;
				if (player.GetComponent<Player>().activeLight.name == "Lens")
				{
					modelRenderer.enabled = false;
					theBoxCollider.center = changedPosition;
				}
				else
				{
					modelRenderer.enabled = true;
					theBoxCollider.center = Vector3.zero;
				}
			}
		}
	}
	
	public void OnTriggerExit(Collider c)
	{
		if (c.name != "Lens")
		{
			if (c.transform.parent.tag == "Player")
			{
				modelRenderer.enabled = true;
				theBoxCollider.center = Vector3.zero;
			}
		}
	}
}
