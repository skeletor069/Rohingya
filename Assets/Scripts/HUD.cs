using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DataHUD
{
	public int health;
	public int energy;
	public int food;
	public float wallet;
	public string heroSkill;
	public string timeText;
	public string daysText;
}

public class HUD : MonoBehaviour {
	private static HUD instance;

	public TextMeshProUGUI daysText;
	public TextMeshProUGUI hoursText;
	public TextMeshProUGUI walletText;
	public TextMeshProUGUI skillsText;
	public AttributeUI healthBar;
	public AttributeUI energyBar;
	public AttributeUI foodBar;
	public Image attributesBG;
	public GameObject careerPanel;

	private DataHUD dataHud;

	private void Awake() {
		instance = this;
	}

	public static HUD GetInstance() {
		return instance;
	}

	void Update() {
		dataHud = GameController.GetInstance().World.GetDataHud();
		daysText.text = dataHud.daysText;
		hoursText.text = dataHud.timeText;
		healthBar.SetValue(dataHud.health);
		energyBar.SetValue(dataHud.energy);
		foodBar.SetValue(dataHud.food);
		walletText.text = ""+dataHud.wallet;
	}

	public void HideHud() {
		daysText.enabled = false;
		hoursText.enabled = false;
		healthBar.Hide();
		energyBar.Hide();
		foodBar.Hide();
		//attributesBG.enabled = false;
		careerPanel.SetActive(false);
	}

	public void ShowHud() {
		daysText.enabled = true;
		hoursText.enabled = true;
		healthBar.Show();
		energyBar.Show();
		foodBar.Show();
		//attributesBG.enabled = true;
		careerPanel.SetActive(true);
	}

	public void ShowTime() {
		daysText.enabled = true;
		hoursText.enabled = true;
	}

}
