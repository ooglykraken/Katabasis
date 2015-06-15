using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public string give;
	public string take;
	
	public string greeting;
	public string trade;
	public string afterTrade;
	
	private bool tradeFinished;
	
	public GameObject mineral;
	
	private static float distanceToGreet = 2.5f;
	
	public void Awake(){
		tradeFinished = false;
	}
	
	public void Update(){
		if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < distanceToGreet){
			Say(trade);
		}
	}
	
	// public void OnTriggerEnter(Collider c){
		// if(c.transform.parent.tag == "Player"){
			// Say(greeting);
		// }
	// }
	
	public void Talk(){
		if(tradeFinished){
			Say(greeting);
		} else {
			
			Trade();
			tradeFinished = true;
			Say(afterTrade);
		}
	}
	
	private void Trade(){
		Debug.Log("trading");
	
		Player p = GameObject.Find("Player").GetComponent<Player>();
		
		if(p.hasDiamond && name == "Zafeiri"){
			p.hasDiamond = false;
			Destroy(GameObject.Find("Player").transform.Find("Diamond"));
			p.hasSapphire = true;
			mineral.GetComponent<MineralBlock>().FollowPlayer();
		} else if(p.hasEmerald && name == "Chrys"){
			p.hasEmerald = false;
			Destroy(GameObject.Find("Player").transform.Find("Emerald"));
			p.hasGold = true;
			mineral.GetComponent<MineralBlock>().FollowPlayer();
		} else if(p.hasSapphire && name == "Agdi"){
			p.hasSapphire = false;
			Destroy(GameObject.Find("Player").transform.Find("Sapphire"));
			p.hasEmerald = true;
			mineral.GetComponent<MineralBlock>().FollowPlayer();
		} else if(p.hasGold && name == "Thura"){
			p.hasGold = false;
			Destroy(GameObject.Find("Player").transform.Find("Gold"));
			OpenDoor();
		} else if(p.hasLantern && name == "Kleis"){
			p.hasLantern = false;
			p.transform.Find("Lantern").gameObject.SetActive(false);
			RevealKey();
		} else {
		}
	}
	
	private void OpenDoor(){
		Transform door = GameObject.Find("Door").transform;
		
		door.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		door.Find("Collider").gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	private void RevealKey(){
		GameObject key = GameObject.Find("Key");
	
		key.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
		key.transform.Find("Collider").gameObject.SetActive(true);
	}
	
	private void Say(string s){
		TextBox.Instance().UpdateText(transform, s);
	}
}
