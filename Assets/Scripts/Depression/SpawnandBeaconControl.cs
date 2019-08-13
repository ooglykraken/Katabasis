using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnandBeaconControl : MonoBehaviour {

	private List<GameObject> beacons;
	private List<GameObject> monsters;
	
	private Light lanternLight;
	
	public float lanternStartingSpotAngle;
	
	private static int numberOfMonsters = 4;
	
	private int monsterIndex;
	
	private int numberOfBeacons;
	
	private bool allBeaconsActivated;
	
	public GameObject monster;
	public GameObject player;
	private GameObject newMonster;
	private Vector3 proposedPos;
	private Vector2 newPos;
	
	public GameObject teleLocation;
	public GameObject key;
	
	public bool hasTeleported;
	private bool allDead;
	
	void Awake()
	{
		hasTeleported = false;
		allBeaconsActivated = false;
		beacons = new List<GameObject>();
		monsters = new List<GameObject>();
		
		// numberOfBeacons = beacons.Count;
		numberOfBeacons = 4;
		
		lanternLight = Player.Instance().transform.Find("Lantern").GetComponent<Light>();
		
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
		
		teleLocation = transform.Find("TeleLocation").gameObject;
		
		// Player.Instance().SetTeleportLocation(teleLocation.transform.position);
	}
	
	public void Start(){
		lanternStartingSpotAngle = lanternLight.spotAngle;
	
		ModifyPlayerLight();
	}
	
	public void Update(){
		
		if(!hasTeleported)
		{
			if(player.GetComponent<Player>().hasFloorKey){
				Teleport();
			}
			
			allDead = false;
		
			foreach(GameObject m in monsters){
				if(m.GetComponent<DepMonsterAI>().isDead){
					allDead = true;
				} else {
					allDead = false;
					break;
				}
			}
		}
		
		if(beacons.Count == numberOfBeacons && !allBeaconsActivated && allDead){
			RevealKey();
			
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
			ModifyPlayerLight();
			SpawnMonster();
		}
	}
	
	public void SpawnMonster()
	{
		monsters[monsterIndex].GetComponent<DepMonsterAI>().Respawn();
		
		monsterIndex++;
		
		if(monsterIndex == numberOfBeacons){
			monsterIndex = 0;
		}
	}
	
	public void RemoveBeacon()
	{
		if(beacons.Count > 0)
		{
			beacons[0].GetComponent<LightBeacon>().ReturnLight();
			beacons.RemoveAt(0);
			ModifyPlayerLight();
		}
	}
	
	private void Teleport(){
		Player.Instance().Teleport(teleLocation.transform.position);
		hasTeleported = true;
	}
	
	private void RevealKey(){
		// GameObject key = GameObject.Find("Key");
		key.SetActive(true);
		Player.Instance().Speak("I know the key is here now");
		
	}
	
	private void ModifyPlayerLight(){
		int lightModifier = 1 + beacons.Count;
		
		lanternLight.intensity = lightModifier;
		lanternLight.spotAngle = (lightModifier / numberOfBeacons) * lanternStartingSpotAngle;
		
		Debug.Log((lightModifier / numberOfBeacons) + ": light mod....");
	}
}
