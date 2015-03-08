using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {
	
	private Vector3 touchPosition;
	private Vector3 startDistanceFromTouch;
	private Vector3 mousePosition;
	
	private bool selected = false;
	
	private float max;
	private float min;
	private float distance;
	
	private float increment;
	
	public GameObject sliderButton;
	public GameObject sliderBar;
	
	public void Awake(){
		sliderButton = transform.Find("SlideButton").gameObject;
		sliderBar = transform.Find("SlideBar").gameObject;
		
		// Unfortunately this will have to be hardcoded til I find a better solution
		
		// max = sliderBar.GetComponent<Renderer>().bounds.max.x;
		// min = sliderBar.GetComponent<Renderer>().bounds.min.x;
		
		min = -.4f;
		max = .4f;
		
		distance = -min + max;
		
		increment = distance / 1f;
	}
	
	public void Start(){
		float offset = (RenderSettings.ambientLight.r * increment) - max;
		
		sliderButton.transform.localPosition = new Vector3(offset, sliderButton.transform.localPosition.y, sliderButton.transform.localPosition.z);
	}
	
	public void FixedUpdate(){
		// max = sliderBar.GetComponent<Renderer>().bounds.max.x;
		// min = sliderBar.GetComponent<Renderer>().bounds.min.x;
	}
	
	public void Update(){
		
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		AdjustValue();
		
		if(!selected){
			selected = Click();
		} else {
			if(Input.GetMouseButtonUp(0)){
				selected = false;
				return;
			}
			
			MoveButton();
		}
		
		// Debug.Log(sliderButton.transform.localPosition + " slider position." + offset + " button offset");
	}
	
	private bool Click(){
		bool clicked = false;
	
		if(OnDrag() && InBounds()){
			clicked = true;
		}
		
		return clicked;
	}
	
	private bool OnDrag(){
		bool onDrag = false;
		
		if(Input.GetMouseButton(0)){
			onDrag = true;
		}
		
		return onDrag;
	}
	
	private bool InBounds(){
		bool inBounds = false;
		
		if(sliderButton.GetComponent<Collider>().bounds.Contains(new Vector3(mousePosition.x, mousePosition.y, sliderButton.transform.position.z)) || sliderBar.GetComponent<Renderer>().bounds.Contains(new Vector3(mousePosition.x, mousePosition.y, sliderBar.transform.position.z))){
			inBounds = true;
		}
		
		return inBounds;
	}
	
	private void MoveButton(){
		
		sliderButton.transform.position = new Vector3(mousePosition.x, sliderButton.transform.position.y, sliderButton.transform.position.z);
	
		if(sliderButton.transform.localPosition.x < min){
			sliderButton.transform.localPosition = new Vector3(min, sliderButton.transform.localPosition.y, sliderButton.transform.localPosition.z);
		} else if(sliderButton.transform.localPosition.x > max){
			sliderButton.transform.localPosition = new Vector3(max, sliderButton.transform.localPosition.y, sliderButton.transform.localPosition.z);
		}
	}
	
	public void AdjustValue(){
		float newValue = ((sliderButton.transform.localPosition.x + max) / increment);
		
		Debug.Log(newValue);
		
		RenderSettings.ambientLight = new Color(newValue, newValue, newValue, RenderSettings.ambientLight.a);
	}
}
