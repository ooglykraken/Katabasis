using UnityEngine;
using System.Collections;

public class Denial : MonoBehaviour {
	
	//The bridges created by the puzzles named
	public Transform platformBridge;
	public Transform boxBridge;

	public GameObject boxArrow;
	public GameObject platformArrow;
	
	private Renderer boxBridgeRenderer;
	private Renderer platformBridgeRenderer;
	
	public Transform platformTeleporter;
	public Transform teleporterSign;
	
	public void Awake(){
		platformBridgeRenderer = platformBridge.transform.Find("Model").gameObject.GetComponent<Renderer>();
		boxBridgeRenderer = boxBridge.transform.Find("Model").gameObject.GetComponent<Renderer>();
	
		platformArrow.SetActive(false);
	}
	
	public void Update(){
		
	}
	
	public void FinishMovingPlatforms(){
		
		
		platformArrow.SetActive(false);
		platformTeleporter.gameObject.SetActive(true);
		teleporterSign.gameObject.SetActive(true);
		
		EnablePiece(platformBridge);
		
	}
	
	public void FinishBoxPuzzle(){

		platformArrow.SetActive(true);
		boxArrow.SetActive(false);
		
		EnablePiece(boxBridge);
	}
	
	private void EnablePiece(Transform piece){
	
		piece.Find("Model").gameObject.GetComponent<MeshRenderer>().enabled = true;
		piece.Find("Collider").gameObject.GetComponent<Collider>().enabled = true;
	}
	
	// private void FadeInPiece(Renderer pieceRenderer, float pieceTimer){
		
		
		// float newAlpha = 1f - (pieceTimer/timeToAppear);
		
		// Debug.Log(newAlpha + " " + pieceTimer);
		
		// if(newAlpha > 1f){
			// newAlpha = 1f;
		// }
		// Color matColor = pieceRenderer.material.color;
		// pieceRenderer.material.color = new Vector4(matColor.r, matColor.g, matColor.b, newAlpha);
	// }
}
