using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	// public string give;
	public string take;
	
	public string greeting;
	public string hint;
	public string trade;
	public string afterTrade;
	
	private bool tradeFinished;
	
	private int sinceTrade = 140;
	public int countdownSinceTrade;
	
	public GameObject item;
	
	public GameObject target;
	
	private static float distanceToGreet = 2.5f;
	
	public void Awake(){
		tradeFinished = false;
		countdownSinceTrade = sinceTrade;
	}
	
	public void Update(){
		if(tradeFinished && countdownSinceTrade > 0){
			countdownSinceTrade--;
		}
		
		if(Vector3.Distance(transform.position, Player.Instance().transform.position) < distanceToGreet){
			if(tradeFinished && countdownSinceTrade == 0){
				Say(trade);
			} else if(!tradeFinished && countdownSinceTrade == sinceTrade){
				Say(hint);
			}
		} else if(Vector3.Distance(transform.position, Player.Instance().transform.position) < (distanceToGreet * 2f)){
			Say(greeting);
		}
	}
	
	// public void OnTriggerEnter(Collider c){
		// if(c.transform.parent.tag == "Player"){
			// Say(greeting);
		// }
	// }
	
	public void Talk(){
		if(!tradeFinished){
		
			// Player p = GameObject.Find("Player").GetComponent<Player>();
		
			bool canTrade = false;
			
			switch(take){
				case("LoveLetter"):
					if(Player.Instance().hasLoveLetter){
						canTrade = true;
					}
					break;
				case("Pocketwatch"):
					if(Player.Instance().hasPocketwatch){
						canTrade = true;
					}
					break;
				case("DogCollar"):
					if(Player.Instance().hasDogCollar){
						canTrade = true;
					}
					break;
				case("TeddyBear"):
					if(Player.Instance().hasTeddyBear){
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
	
		Player p = Player.Instance();
		
		if(p.hasLoveLetter && name == "Zafeiri"){
			p.hasLoveLetter = false;
			GameObject b = GameObject.Find("Bargaining/Items/LoveLetter");
			
			SpriteOrderer.Instance().allSpriteRenderers.Remove(b.transform.Find("Sprite").GetComponent<SpriteRenderer>());
			Destroy(b);
			
			p.hasTeddyBear = true;
			item.GetComponent<BargainingItem>().FollowPlayer();
		} else if(p.hasPocketwatch && name == "Chrys"){
			p.hasPocketwatch = false;
			
			GameObject b = GameObject.Find("Bargaining/Items/Pocketwatch");
			
			SpriteOrderer.Instance().allSpriteRenderers.Remove(b.transform.Find("Sprite").GetComponent<SpriteRenderer>());
			Destroy(b);
	
			p.hasDogCollar = true;
			item.GetComponent<BargainingItem>().FollowPlayer();
		} else if(p.hasTeddyBear && name == "Agdi"){
			p.hasTeddyBear = false;
			
			GameObject b = GameObject.Find("Bargaining/Items/TeddyBear");
			
			SpriteOrderer.Instance().allSpriteRenderers.Remove(b.transform.Find("Sprite").GetComponent<SpriteRenderer>());
			Destroy(b);
	
			

			p.hasPocketwatch = true;
			item.GetComponent<BargainingItem>().FollowPlayer();
		} else if(p.hasDogCollar && name == "Thura"){
			p.hasDogCollar = false;

			GameObject b = GameObject.Find("Bargaining/Items/DogCollar");
			
			SpriteOrderer.Instance().allSpriteRenderers.Remove(b.transform.Find("Sprite").GetComponent<SpriteRenderer>());
			Destroy(b);
			OpenDoor();
		} else if(p.hasLantern && name == "Kleis"){
			p.hasLantern = false;
			p.transform.Find("Lantern").gameObject.SetActive(false);
			RevealKey();
		} else {
		}
	}
	
	private void OpenDoor(){
		
		target.transform.Find("Model").gameObject.GetComponent<Renderer>().enabled = false;
		target.transform.Find("Collider").gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	private void RevealKey(){
		// GameObject key = GameObject.Find("Key");
	
		target.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
		target.transform.Find("Collider").gameObject.SetActive(true);
	}
	
	private void Say(string s){
		TextBox.Instance().UpdateText(transform, s);
	}
}
