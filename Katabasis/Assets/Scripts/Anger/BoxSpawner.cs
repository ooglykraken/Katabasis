using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour {

	public GameObject Box;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnBox());
	}
	
	public IEnumerator SpawnBox()
	{
		GameObject.Instantiate(Box, this.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		StartCoroutine(SpawnBox());
	}
}
