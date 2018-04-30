using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {
	private string name;
	public void SaveP(){
		
		for (int num = 0; num < GameManager.instance.AllButtons.Count;num++)
		{
			name = "Button " + num.ToString () + " Winnings";
		PlayerPrefs.SetInt(name,GameManager.instance.AllButtons[num].GetComponent<Buttons>().Winnings);
		name = "Button " + num.ToString()+ " Loses";
		PlayerPrefs.SetInt(name,GameManager.instance.AllButtons[num].GetComponent<Buttons>().Loses);
	}
	}

	public void Load(){
		for (int num = 0; num < GameManager.instance.AllButtons.Count;num++)
		{
		name = "Button " + num.ToString () + " Winnings";
			GameManager.instance.AllButtons[num].GetComponent<Buttons>().Winnings = PlayerPrefs.GetInt(name);
			 	name = "Button " + num.ToString() + " Loses";
			GameManager.instance.AllButtons[num].GetComponent<Buttons>().Loses =	PlayerPrefs.GetInt(name);

			GameManager.instance.AllButtons [num].GetComponent<Buttons> ().changeText ();

	}
		GameManager.instance.GivePrize ();
		GameManager.instance.ResetButton.SetActive (true);

	}
}

