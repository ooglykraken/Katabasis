using UnityEngine;
using System.Collections;

public class TempLoadLevel : MonoBehaviour {

	public void OnCollisionEnter(Collision c)
	{
		Application.LoadLevel ("Credits");
	}
}
