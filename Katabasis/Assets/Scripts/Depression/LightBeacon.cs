using UnityEngine;
using System.Collections;

public class LightBeacon : MonoBehaviour {

	public GameObject beaconControl;
	public Sprite onSprite;
	public Sprite offSprite;
	
	private bool isOn;
	
	public void Awake(){
		isOn = true;
	}
	
	public void TakeLight()
	{
		if (isOn)
		{
			Debug.Log("Taking");
			transform.FindChild("Light").gameObject.SetActive(false);
			transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
			beaconControl.GetComponent<SpawnandBeaconControl>().AddBeaconAndSpawnEnemy(gameObject);
			isOn = false;
		}
	}
	
	public void ReturnLight()
	{
		transform.FindChild("Light").gameObject.SetActive(true);
		transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
		isOn = true;
	}
}
