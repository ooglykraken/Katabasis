using UnityEngine;
using System.Collections;

public class BoyWalkingAnimation : MonoBehaviour {
	
	private Animator animator;
	
	private Player player;
	
	// private bool isWalking;
	
	public void Awake(){
		
		animator = gameObject.GetComponent<Animator>();
		
		player = GameObject.Find("Player").GetComponent<Player>();
		
	}
	
	public void Update(){
		if(player.isWalking){
			// Debug.Log("True");
			animator.SetBool("isWalking", true);
		} else { 
			animator.SetBool("isWalking", false);
		}
	}
}
