using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	// public string give;
	public string take;
	
	public string greeting;
	public string trade;
	public string afterTrade;
	
	private bool tradeFinished;
	
	private int sinceTrade = 100;
	public int countdownSinceTrade;
	
	public GameObject mineral;
	
	private static float distanceToGreet = 2.5f;
	
	public void Awake(){
		tradeFinished = false;
		countdownSinceTrade = sinceTrade;
	}
	
	public void Update(){
		if(tradeFinished && countdownSinceTrade > 0){
			countdownSinceTrade--;
		}
		
		if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < distanceToGreet){
			if(tradeFinished && countdownSinceTrade == 0){
				Say(greeting);
			} else if(!tradeFinished && countdownSinceTrade == sinceTrade){
				Say(trade);
			}
		}
	}
	
	// public void OnTriggerEnter(Collider c){
		// if(c.transform.parent.tag == "Player"){
			// Say(greeting);
		// }
	// }
	
	public void Talk(){
		if(!tradeFinished){
		
			Player p = GameObject.Find("Player").GetComponent<Player>();
		
			bool canTrade = false;
			
			switch(take){
				case("Diamond"):
					if(p.hasDiamond){
						canTrade = true;
					}
					break;
				case("Gold"):
					if(p.hasGold){
						canTrade = true;
					}
					break;
				case("Sapphire"):
					if(p.hasSapphire){
						canTrade = true;
					}
					break;
				case("Emerald"):
					if(p.hasEmerald){
						canTrade = true;
					}
					break;
				case("Light"):
					canTrade = true;
					break;
				default:
					break;
			}
			if(canTrade){
				Trade();
				tradeFinished = true;
				Say(afterTrade);
			}
		}
	}
	
	private void Trade(){
		Debug.Log("trading");
	
		Player p = GameObject.Find("Player").GetComponent<Player>();
		
		if(p.hasDiamond && name == "Zafeiri"){
			p.hasDiamond = false;
			Destroy(GameObject.Find("Bargaining/Minerals/Diamond Block"));
			p.hasSapphire = true;
			mineral.GetComponent<MineralBlock>().FollowPlayer();
		} else if(p.hasEmerald && name == "Chrys"){
			p.hasEmerald = false;
			Destroy(GameObject.Find("Bargaining/Minerals/Emerald Block"));
			p.hasGold = true;
			mineral.GetComponent<MineralBlock>().FollowPlayer();
		} else if(p.hasSapphire && name == "Agdi"){
			p.hasSapphire = false;
			Destroy(GameObject.Find("Bargaining/Minerals/Sapphire Block"));
			p.hasEmerald = true;
			mineral.GetComponent<MineralBlock>().FollowPlayer();
		} else if(p.hasGold && name == "Thura"){
			p.hasGold = false;
			Destroy(GameObject.Find("Bargaining/Minerals/Gold Block"));
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
