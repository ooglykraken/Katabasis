﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnandBeaconControl : MonoBehaviour {

	private List<GameObject> beacons;
	private List<GameObject> monsters;
	
	
	public GameObject monster;
	public GameObject player;
	private GameObject newMonster;
	private Vector3 proposedPos;
	private Vector2 newPos;
	
	public GameObject teleLocation;
	
	
	void Awake()
	{
		beacons = new List<GameObject>();
		monsters = new List<GameObject>();
	}
	
	void FixedUpdate()
	{
		if(beacons.Count == 7)
		{
			player.transform.position = teleLocation.transform.position;
		}
	}
	
	public void AddBeaconAndSpawnEnemy(GameObject beacon)
	{
		beacons.Add(beacon);
		SpawnMonster();
	}
	
	public void SpawnMonster()
	{
		Debug.Log ("SpawnMonster called");
		proposedPos =  Random.onUnitSphere * 16;
		while(Vector3.Distance(proposedPos, player.transform.position) < 14) {
			proposedPos =  Random.onUnitSphere * 16;
		}
		newPos = proposedPos;
		newMonster = (GameObject) Instantiate (monster, newPos, monster.transform.rotation);
		newMonster.GetComponent<DepMonsterAI>().player = player;
		newMonster.GetComponent<DepMonsterAI>().spawnControl = gameObject;
	}
	
	public void RemoveBeacon()
	{
		if(beacons.Count > 0)
		{
			beacons[0].GetComponent<LightBeacon>().ReturnLight();
			beacons.RemoveAt(0);
		}
	}
}