using UnityEngine;
using System.Collections;

public class SmokeEnemy : MonoBehaviour {


/*
       Make Smoke Enemy stay instead of destroying itself after hitting player, so that it is more a piece of a puzzle.
       Reorganize the collider and script for Smoke Enemy and Lantern Oil to be more like the player set up. 
       Add Smoke Enemy to the floor switch.
       Smoke Enemy is "on" the player when it hits the player, and clicking the player throws the Smoke Enemy off him.
       Make Smoke Enemy wander to random locations while not in range of the player.
       Make Smoke Enemy avoid objects.
*/



	
	private Vector3 playerPosition;
	private Vector3 enemyPosition;
	private Vector3 startPosition;
	public bool isHitByLight;
	public bool thrownOff;
	private float distanceToPlayer;
	public float startEnemySpeed;
	private float enemySpeed;
	private GameObject player;
	private float stunTimer;
	public float startStunTimer;
	public float attractionDistance;
	
	
	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
		isHitByLight = false;
		player = GameObject.Find ("Player");
		enemySpeed = 1.25f;
		stunTimer = 300f;
		thrownOff = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		enemyPosition = transform.position;
		playerPosition = player.transform.position;
		distanceToPlayer = Vector3.Distance (playerPosition, enemyPosition);
		
		if ((distanceToPlayer <= attractionDistance) && (isHitByLight == false))
		{
			transform.LookAt (playerPosition);
			transform.Translate (Vector3.forward * enemySpeed * Time.deltaTime);
		}
		
		if (isHitByLight == true && stunTimer > 0f)
		{
			stunTimer--;
			enemySpeed = 0f;
		}
		else
		{
			stunTimer = startStunTimer;
			isHitByLight = false;
			enemySpeed = startEnemySpeed;
		}
		
		if (thrownOff == true)
		{
			transform.parent = player.transform.parent;
			Reappear ();
			thrownOff = false;
		}
	}
	
	public void OnCollisionEnter(Collision c)
	{
		if (c.transform.tag.Equals("Player"))
		{
			Debug.Log ("Hit Player. Now begin slow and smoke disperse animation.");
			GetComponentInChildren<MeshRenderer>().enabled = false;
			GetComponentInChildren<Collider>().enabled = false;
			transform.parent = player.transform;
		}
		else
		{
		}
		
	}
	
	private void Reappear()
	{
		transform.position = startPosition;
		GetComponentInChildren<MeshRenderer>().enabled = true;
		GetComponentInChildren<Collider>().enabled = true;
	}
}
