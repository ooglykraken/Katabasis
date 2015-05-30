using UnityEngine;
using System.Collections;

public class LightBeacon : MonoBehaviour {

	public GameObject beaconControl;
	public Sprite onSprite;
	public Sprite offSprite;
	
	private bool isOn = true;

	public void TakeLight()
	{
		if (isOn)
		{
			transform.FindChild("Light").gameObject.SetActive(false);
			gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
			beaconControl.GetComponent<SpawnandBeaconControl>().AddBeaconAndSpawnEnemy(gameObject);
			isOn = false;
		}
	}
	
	public void ReturnLight()
	{
		transform.FindChild("Light").gameObject.SetActive(true);
		gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
		isOn = true;
	}
}
