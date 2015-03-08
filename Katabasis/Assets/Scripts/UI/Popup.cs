using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	public void Start(){
		transform.parent = Camera.main.transform;
			
		transform.localPosition = new Vector3(0f, 0f, 3f);
	}
	
	public void Update(){
	
		if(Input.GetKeyDown("escape") || PlayerClicksOutOfBounds()){
			Close();
		}
	}
	
	private void Close(){
		Debug.Log("Popup being closed");
		Gameplay.Instance().popupOpen = false;
		transform.Find("Slider").GetComponent<Slider>().AdjustValue();
		Destroy(gameObject);
	}
	
	private bool PlayerClicksOutOfBounds(){
		Vector3 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseInput = new Vector3(mouseInput.x, mouseInput.y, transform.position.z);
		Bounds colliderBounds = gameObject.GetComponent<Collider>().bounds;
		
		bool outOfBounds = false;
		if(Input.GetMouseButtonDown(0)){
			// if(mouseInput.x > colliderBounds.max.x || mouseInput.x < colliderBounds.min.x || mouseInput.y > colliderBounds.max.y || mouseInput.y < colliderBounds.min.y){
				// outOfBounds = true;
			// }
			
			if(!colliderBounds.Contains(mouseInput)){
				outOfBounds = true;
			}
		}
		
		return outOfBounds;
	}
}
