using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	
	private float distance = 14.7f;
	
	private int timeToWait = 100;
	public int timer;
	
	public bool isAtRight;
	public bool isAtLeft;
	
	private Vector3 startPosition;
	// Use this for initialization
	public void Awake () {
		startPosition = transform.localPosition;
		
		timer = timeToWait;
		isAtLeft = true;
	}
	
	// public void Start(){
		// isAtLeft = true;
	// }
	
	// Update is called once per frame
	public void Update () {
		if(timer <= 0){
			if(isAtRight){
				MoveLeft();
			} else if(isAtLeft) {
				MoveRight();
			}
		} else {
			timer --;
		}
	}
	
	private void MoveRight(){
		Vector3 rightExtreme = startPosition + new Vector3(distance, 0f, 0f);
		
		if(Vector3.Distance(transform.localPosition, rightExtreme) > .1f){
			transform.localPosition = Vector3.Lerp(transform.localPosition, rightExtreme, Time.deltaTime * 1f);
			// Debug.Log("going RIght");
			// isAtLeft = false;
		} 
		if(Vector3.Distance(transform.localPosition, rightExtreme) < .1f) {
			timer = timeToWait;
			// Debug.Log("starting left");
			isAtRight = true;
			isAtLeft = false;
		}
	}
	private void MoveLeft(){
		if(Vector3.Distance(transform.localPosition, startPosition) > .1f){
			transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * 1f);
			// isAtRight = false;
		} 
		if(Vector3.Distance(transform.localPosition, startPosition) < .1f) {
			timer = timeToWait;
			isAtLeft = true;
			isAtRight = false;
		}
	}
}
