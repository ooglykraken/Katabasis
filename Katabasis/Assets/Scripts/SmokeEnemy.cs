using UnityEngine;
using System.Collections;

public class SmokeEnemy : MonoBehaviour {
	
	private Vector3 playerPosition;
	private Vector3 enemyPosition;
	public bool isHitByLight;
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
		isHitByLight = false;
		player = GameObject.Find ("Player");
		enemySpeed = 1.25f;
		stunTimer = 300f;
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
	}
	
	public void OnCollisionEnter(Collision c)
	{
		if (c.transform.tag.Equals("Player"))
		{
			Debug.Log ("Hit Player. Now begin slow and smoke disperse animation.");
			Destroy(gameObject);
		}
		else
		{
		}
		
	}
}
