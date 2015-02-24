using UnityEngine;
using System.Collections;

public class MultiFloorSwitch : MonoBehaviour {

	public int numberOfSwitches;
	private int counter;
	
	public GameObject target;
	
	public string function;
	public string argument;
	
	bool isFlipped;
	
	public void FixedUpdate()
	{
		if (counter == numberOfSwitches)
		{
			ActivateSwitch();
		}
	}
	
	private void ActivateSwitch(){
		if(isFlipped){
			return;
		}
		
		isFlipped = true;
		
		if (target) {
			if (function.Length > 0) {
				if (argument.Length > 0)
					target.SendMessage(function, argument, SendMessageOptions.DontRequireReceiver);
				else
					target.SendMessage(function, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	public void CountUp()
	{
		counter += 1;
	}
	public void CountDown()
	{
		counter -= 1;
	}
}
