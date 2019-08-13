using UnityEngine;
using System.Collections;

public class AnimateConveyorBelt : MonoBehaviour {
	
	private Material beltMaterial;
	
	// private int timeTilChange = .2;
	private int countdown;
	
	public void Start(){
		beltMaterial = gameObject.GetComponent<Renderer>().material;
	}
	
	public void Update(){
		
		// if(countdown <= 0){
			beltMaterial.mainTextureOffset += new Vector2(0f, .01f);
			gameObject.GetComponent<Renderer>().material = beltMaterial;
			// countdown = timeTilChange;
		// } else {
			// countdown--;
		// }
		
		if(beltMaterial.mainTextureOffset.y > 100f){
			beltMaterial.mainTextureOffset = new Vector2(0f, 0f);
		}
		
	}
}
