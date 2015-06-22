using UnityEngine;
using System.Collections;

public class DepMonsterAI : MonoBehaviour {

	public GameObject player;
	public GameObject spawnControl;
	
	private SpriteRenderer spriteRenderer;
	
	private new Collider collider;
	
	public bool isDead;
	
	// private Vector3 proposedPos;
	// private Vector2 newPosition;
	
	public void Awake(){
		isDead = false;
		
		spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		collider = transform.Find("Collider").gameObject.GetComponent<Collider>();
	}
	
	public void FixedUpdate()
	{
		if(!isDead){
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .03f);
		}
	}
	
	public void OnCollisionEnter(Collision c)
	{
		if (c.transform.tag == "Player")
		{
			spawnControl.GetComponent<SpawnandBeaconControl>().RemoveBeacon();
			Kill();
		} else if(c.transform.tag == "LightBeacon"){
		}
	}
	
	public void Respawn()
	{	
		isDead = false;
		
		
		Vector3 proposedPos =  Random.onUnitSphere * 16;
		while(Vector3.Distance(proposedPos, player.transform.position) < 14) {
			proposedPos =  Random.onUnitSphere * 16;
		}
		// Vector2 newPosition = proposedPos;
		transform.position = new Vector3(proposedPos.x, proposedPos.y, -.5f);
		spriteRenderer.enabled = true;
		collider.enabled = true;
		
		SpriteOrderer.Instance().allSpriteRenderers.Add(transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		
		// monsterIndex++;;
	}
	
	public void Kill(){
		isDead = true;
		
		spriteRenderer.enabled = false;
		collider.enabled = false;
	}
}
