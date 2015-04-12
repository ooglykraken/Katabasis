using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public GameObject want;
	public GameObject itemToGive;
	public string message;
	public string endMessage;
	
	public GameObject player;
	private bool haveWant;

	public void Talk()
	{
		if (!haveWant)
		{
			TextBox.Instance().UpdateText(message);
		}
		else
		{
			TextBox.Instance().UpdateText(endMessage);
			Give();
		}
		if (want.name == "Lantern")
		{
			takeLight();
			TextBox.Instance().UpdateText(endMessage);
			Give();
		}
	}
	
	public void OnTriggerEnter(Collider c)
	{
		if (c.transform.gameObject.name == want.name)
		{
			haveWant = true;
		}
	}
	
	private void takeLight()
	{
		player.GetComponent<Player>().hasLantern = false;
		player.transform.FindChild ("Lantern").gameObject.SetActive(false);
	}
	
	private void Give()
	{
		if(itemToGive.name != "Door")
		{
			itemToGive.transform.position = new Vector3(itemToGive.transform.position.x, itemToGive.transform.position.y, -.01f);
		}
		else
		{
			itemToGive.GetComponent<Door>().Destroy ();
		}
	}

}
