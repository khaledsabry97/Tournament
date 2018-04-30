using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour {
	[SerializeField]	private GameObject prize;

	private bool found;
	private bool FirstClick = false;
	private int loses;
	private int winnings;
	private Text[] winandlose;
	private GameObject[] winlosttext;






	public int Loses{
		get{ return loses; }
		set{loses = value;}
	}
	public int Winnings{
		get{ return winnings; }
		set{ winnings = value; }
	}
	public GameObject Prize{
		get{ return prize; }
	}



	void Start () {
		GameManager.instance.AllButtons.Add (this.gameObject);
		winandlose = GetComponentsInChildren<Text> ();
	}

	public void Clicked(){
		
		if (GameManager.instance.State == State.FirstSelect && !FirstClick) {
			FirstClick = true;
			GameManager.instance.Gamers.Add (this.gameObject);
			GameManager.instance.ChangePos (this.gameObject);
		} else if (GameManager.instance.State == State.FirstSelect) {
			FirstClick = false;
			GameManager.instance.StartPos (this.gameObject);
			GameManager.instance.Gamers.Remove (this.gameObject);
		}
			
	else if (GameManager.instance.State == State.winner) {
			GameManager.instance.changewinningandlosses (this.gameObject);
			FirstClick = false;
			GameManager.instance.Clicks++;
			if (GameManager.instance.Clicks == GameManager.instance.Gamers.Count - 1) {
				GameManager.instance.GivePrize();
				GameManager.instance.Reset ();
			}
			else {
			GameManager.instance.AddNewWinner (this.gameObject);
			print (GameManager.instance.Clicks);
		}
		}
	}

	public void changeText(){
		winandlose [0].text = winnings.ToString();
		winandlose [1].text = loses.ToString();

	}

	}

	
