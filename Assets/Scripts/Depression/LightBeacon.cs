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
			// SpriteOrderer.Instance().allSpriteRenderers.Remove(transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
			transform.Find("Light").gameObject.SetActive(false);
			transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
			// SpriteOrderer.Instance().allSpriteRenderers.Add(transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
			
			beaconControl.GetComponent<SpawnandBeaconControl>().AddBeaconAndSpawnEnemy(gameObject);
			
			isOn = false;
		}
	}
	
	public void ReturnLight()
	{
	
		// SpriteOrderer.Instance().allSpriteRenderers.Remove(transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		transform.Find("Light").gameObject.SetActive(true);
		transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
		// SpriteOrderer.Instance().allSpriteRenderers.Add(transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		
		isOn = true;
	}
}
