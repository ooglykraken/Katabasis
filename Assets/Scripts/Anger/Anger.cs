using UnityEngine;
using System.Collections;

public class Anger : MonoBehaviour {

	public GameObject ferry;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void FinishRunPuzzle(){
		ferry.SetActive(true);
	}
}
