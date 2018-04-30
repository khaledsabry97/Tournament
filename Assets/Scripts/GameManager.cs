using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State{
	FirstSelect,winner,

}
public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;

	//[SerializeField]  private GameObject[] Players;
	[SerializeField] GameObject ARENA;
	[SerializeField] private GameObject round;
	[SerializeField] private GameObject buttonStart;
	[SerializeField] private GameObject buttonNextRound;
	[SerializeField] private GameObject start;
	[SerializeField] private GameObject resetbutton;
	[SerializeField] private GameObject tournment;

	private GameObject[] winlosttext;
	private List<GameObject> allButtons = new List<GameObject>();
	private List<GameObject> gamers = new List<GameObject> ();
	private List<GameObject> fighters = new List<GameObject> ();
	private List<GameObject> winners = new List<GameObject> ();
	private State state = State.FirstSelect;
	private int i = 0;
	private bool resetsettings = false;
	private bool winnersettings = false;
	private GameObject player1;
	private GameObject player2;
	private int clicks = 0;
	private bool found = false;
	private GameObject best = null;
	private bool reseted = true;
	private GameObject[] SaveAndLoad;




	public GameObject Tournment{
		get{ return tournment; }
	}

	public bool Found{
		get{ return found; }
		set{ found = value; }
	}


	public int Clicks{
		get{ return clicks; }
		set{ clicks = value; }
	}


	public State State  {
		get{ return state; }
	}

	public List<GameObject> AllButtons{
		get{ return allButtons; }
	}

	public List<GameObject> Gamers{
		get{ return gamers; }
	}

	public List<GameObject> Winners{
		get{ return winners; }
	}

	public GameObject ResetButton{
		get{ return resetbutton; }
		set{ resetbutton = value; }

	}

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
			DontDestroyOnLoad (gameObject);
	}


	void Start () {
		winlosttext = GameObject.FindGameObjectsWithTag ("winlose");
		SaveAndLoad = GameObject.FindGameObjectsWithTag ("Save");
	}
	
	// Update is called once per frame
	void Update () {
		if (gamers.Count >= 3 && state == State.FirstSelect)
			buttonStart.SetActive (true);
		if (gamers.Count < 3 && state == State.FirstSelect)
			buttonStart.SetActive (false);
	}

	public void Reset()
	{
		if (!reseted)
			resetbutton.SetActive (true);
			
		foreach (GameObject v in allButtons) {
			v.transform.parent = start.transform;
			v.SetActive (true);

		}
		for(int i = 0 ; i< SaveAndLoad.Length ;i++)
		{
			SaveAndLoad[i].SetActive (true);
		}
		for(int i = 0 ; i< winlosttext.Length ;i++)
		{
			winlosttext[i].SetActive (true);
		}
		resetsettings = false;
		fighters.Clear ();
		gamers.Clear ();
		winners.Clear ();
		state = State.FirstSelect;
		winnersettings = false;
		clicks = 0;
		found = false;
	}
		
	public void ChangePos(GameObject person){
		person.transform.parent = ARENA.transform;
	}

	public void StartPos(GameObject person){
		person.transform.parent = start.transform;
	}


	public void TornmentMembers(){
		resetbutton.SetActive (false);

		if (best != null)
			best.GetComponent<Buttons> ().Prize.SetActive (false);
		suffle();
		foreach (GameObject v in gamers) {
			fighters.Add (v);
		}
		state = State.winner;
		buttonStart.gameObject.SetActive (false);
		foreach (GameObject v in allButtons) {
			v.SetActive (false);

		}
		for(int i = 0 ; i< winlosttext.Length ;i++)
		{
			winlosttext[i].SetActive (false);
		}
		for(int i = 0 ; i< SaveAndLoad.Length ;i++)
		{
			SaveAndLoad[i].SetActive (false);
		}

		foreach (GameObject s in fighters) {
			s.transform.parent = round.transform;	
		}

		BeginNewRound ();
	}

	public void suffle(){
		for (int i = 0; i < gamers.Count; i++) {
			GameObject obj = gamers [i];
			int first = Random.Range (0, gamers.Count);
			gamers [i] = gamers [first];
			gamers [first] = obj;
		}
	}


	public void  AddNewWinner(GameObject win){
		reseted = false;
		fighters [0].SetActive (false);
		fighters [1].SetActive (false);
		player1.SetActive (false);
		player2.SetActive (false);

			foreach (GameObject vK in fighters) {
			if (vK == win) {
				winners.Add (vK);
			}
		}

			if (winners.Count == 3) {
			winnersettings = true;
			GameObject gam = null;
			foreach (GameObject game in winners) {
				if (player2 == game)
					gam = game;
			}
			if (gam != null) {
				winners.Remove (gam);
			}
			}
			GameObject rem1 = null;
			GameObject rem2 = null;
			foreach (GameObject GAM in fighters.ToArray()) {
				if (GAM == player1 || GAM == player2){
					if (rem1 == null) {
						rem1 = GAM;
					} else if (rem2 == null) {
						rem2 = GAM;
					}
						}
			}

		if (rem1 != null) {

			fighters.Remove (rem1);

		}
		if (rem2 != null) {

			fighters.Remove (rem2);
		}
		print ("fighters : " + fighters.Count + " winners : " + winners.Count);
		BeginNewRound ();

	}
		
	public void BeginNewRound ()
	{
			 if (fighters.Count > 1) {
			player1 = fighters [0];
			player2 = fighters [1];
			fighters [0].SetActive (true);
			fighters [1].SetActive (true);
		} else if (fighters.Count == 1) {
			foreach (GameObject game in fighters) {
				if(game != null)

					{
						player1 = fighters [0];
						player1.SetActive (true);
					}
			}

			int num = Random.Range (0, winners.Count);
			fighters.Add (winners [num]);
			fighters [1].SetActive (true);

			player2 = fighters [1];
		} else if (fighters.Count == 0) {

			player1 = winners [0];
			player2 = winners [1];
			player1.SetActive (true);
			player2.SetActive (true);
		}


	}


	public void changewinningandlosses(GameObject win){
		if (player1 != null && player2 != null) {
			if (player1 != win)
				player1.GetComponent<Buttons> ().Loses = player1.GetComponent<Buttons> ().Loses + 1;
			else if(player2 != win)
				player2.GetComponent<Buttons> ().Loses = player2.GetComponent<Buttons> ().Loses + 1;


			win.GetComponent<Buttons> ().Winnings = win.GetComponent<Buttons> ().Winnings + 1;


			player1.GetComponent<Buttons> ().changeText ();
			player2.GetComponent<Buttons> ().changeText ();


		}

	}
		

	public void ResetRecords(){
		foreach (GameObject v in allButtons) {
			v.GetComponent<Buttons> ().Winnings = 0;
			v.GetComponent<Buttons> ().Loses = 0;
			v.GetComponent<Buttons> ().changeText ();
		}
		reseted = true;
		resetbutton.SetActive (false);

		if(best != null)
			best.GetComponent<Buttons> ().Prize.SetActive (false);
		

	}

	public void GivePrize(){
		int Max = 0;
		if(best != null)
			best.GetComponent<Buttons> ().Prize.SetActive (false);
		best = null;
		foreach (GameObject m in allButtons) {
			if ((m.GetComponent<Buttons> ().Winnings - m.GetComponent<Buttons> ().Loses) > Max) {
				Max = (m.GetComponent<Buttons> ().Winnings - m.GetComponent<Buttons> ().Loses);
				best = m;
			}
			else if(((m.GetComponent<Buttons> ().Winnings - m.GetComponent<Buttons> ().Loses) == Max) && Max >0)
				{
				if(m.GetComponent<Buttons> ().Winnings > best.GetComponent<Buttons>().Winnings)
					best = m;


				}

		}

		if (best != null)
			best.GetComponent<Buttons> ().Prize.SetActive (true);
			


	}


}
	
	



