using UnityEngine;
using System.Collections;

public class Denial : MonoBehaviour {
	
	public Transform runPiece;
	public Transform boxPiece;
	public Transform platformPiece;
	
	public void FinishRunPuzzle(){
		Debug.Log("Finished!");
	
		GameObject.Find("Player").GetComponent<Player>().Teleport();
		
		EnablePiece(runPiece);
	}
	
	public void FinishMovingPlatforms(){
		GameObject.Find("Player").GetComponent<Player>().Teleport();
		
		EnablePiece(platformPiece);
	}
	
	public void FinishBoxPuzzle(){
		GameObject.Find("Player").GetComponent<Player>().Teleport();
		
		EnablePiece(boxPiece);
	}
	
	private void EnablePiece(Transform piece){
	
		piece.Find("Model").gameObject.GetComponent<MeshRenderer>().enabled = true;
		piece.Find("Collider").gameObject.GetComponent<Collider>().enabled = true;
	}
}
