using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnandBeaconControl : MonoBehaviour {

	private List<GameObject> beacons;
	private List<GameObject> monsters;
	
	private static int numberOfMonsters = 7;
	
	private int monsterIndex;
	
	private bool allBeaconsActivated;
	
	public GameObject monster;
	public GameObject player;
	private GameObject newMonster;
	private Vector3 proposedPos;
	private Vector2 newPos;
	
	public GameObject teleLocation;
	
	public bool hasTeleported;
	
	void Awake()
	{
		hasTeleported = false;
		allBeaconsActivated = false;
		beacons = new List<GameObject>();
		monsters = new List<GameObject>();
		
		monsterIndex = 0;
		// monsters.Clear();
		for(int i = 0; i < numberOfMonsters; i++){
			newMonster = Instantiate(Resources.Load("Depression/DepressionMonster", typeof(GameObject)) as GameObject) as GameObject;
			newMonster.GetComponent<DepMonsterAI>().player = player;
			newMonster.GetComponent<DepMonsterAI>().spawnControl = gameObject;
			monsters.Add(newMonster);
			newMonster.GetComponent<DepMonsterAI>().Kill();
			newMonster.GetComponent<DepMonsterAI>().isDead = false;
			// SpriteOrderer.Instance().allSpriteRenderers.Add(newMonster.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>());
		}
	}
	
	public void Update(){
		if(!hasTeleported && player.GetComponent<Player>().hasFloorKey)
		{
			bool willTeleport = false;
		
			foreach(GameObject m in monsters){
				if(m.GetComponent<DepMonsterAI>().isDead){
					willTeleport = true;
				} else {
					willTeleport = false;
					break;
				}
			}
			
			if(willTeleport){
				Gameplay.Instance().spawnLocation.gameObject.GetComponent<AudioSource>().Play();
				player.transform.position = teleLocation.transform.position;
				// player.GetComponent<Player>().Teleport();
				hasTeleported = true;
			}
		}
		
		if(beacons.Count == 7 && !allBeaconsActivated){
			RevealKey();
			TextBox.Instance().UpdateText("I know the key is here now");
			allBeaconsActivated = true;
		}
	}
	
	public void FixedUpdate()
	{
		
	}
	
	public void AddBeaconAndSpawnEnemy(GameObject beacon)
	{
		if(!allBeaconsActivated){
			beacons.Add(beacon);
			SpawnMonster();
		}
	}
	
	public void SpawnMonster()
	{
		monsters[monsterIndex].GetComponent<DepMonsterAI>().Respawn();
		
		monsterIndex++;
		
		if(monsterIndex == 7){
			monsterIndex = 0;
		}
	}
	
	public void RemoveBeacon()
	{
		if(beacons.Count > 0)
		{
			beacons[0].GetComponent<LightBeacon>().ReturnLight();
			beacons.RemoveAt(0);
		}
	}
	
	private void RevealKey(){
		GameObject key = GameObject.Find("Key");
	
		key.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
		key.transform.Find("Collider").GetComponent<Collider>().enabled = true;
	}
}
