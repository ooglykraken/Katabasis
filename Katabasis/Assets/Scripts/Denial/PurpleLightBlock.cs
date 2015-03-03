using UnityEngine;
using System.Collections;

public class PurpleLightBlock : MonoBehaviour {

	private GameObject thisBox;
	private BoxCollider theBoxCollider;
	private MeshRenderer modelRenderer;
	private GameObject player;
	private Vector3 normalPosition = new Vector3(0, 0, -1.5f);
	
	
	public void Awake()
	{
		thisBox = transform.parent.gameObject;
		theBoxCollider = thisBox.GetComponentInChildren<BoxCollider>();
		modelRenderer = thisBox.GetComponentInChildren<MeshRenderer>();
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
					modelRenderer.enabled = true;
					theBoxCollider.center = Vector3.zero;
				}
				else
				{
					modelRenderer.enabled = false;
					theBoxCollider.center = normalPosition;
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
				modelRenderer.enabled = false;
				theBoxCollider.center = normalPosition;
			}
		}
	}
}
