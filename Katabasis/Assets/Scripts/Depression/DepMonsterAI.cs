using UnityEngine;
using System.Collections;

public class DepMonsterAI : MonoBehaviour {

	public GameObject player;
	public GameObject spawnControl;
	
	// private Vector3 proposedPos;
	// private Vector2 newPosition;

	void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .03f);
	}
	
	public void OnCollisionEnter(Collision c)
	{
		if (c.transform.tag == "Player")
		{
			spawnControl.GetComponent<SpawnandBeaconControl>().RemoveBeacon();
			Destroy (gameObject);
		}
	}
	
	public void Respawn()
	{
		Vector3 proposedPos =  Random.onUnitSphere * 16;
		while(Vector3.Distance(proposedPos, player.transform.position) < 14) {
			proposedPos =  Random.onUnitSphere * 16;
		}
		// Vector2 newPosition = proposedPos;
		transform.position = proposedPos;
	}
}
