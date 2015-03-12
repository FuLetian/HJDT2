using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject menu1Panel;
	public GameObject menu2Panel;
	public GameObject menu3Panel;
	public GameObject hardSelectorPanel;

	// Use this for initialization
	void Start () {
	
		menu1Panel.SetActive (true);
		menu2Panel.SetActive (false);
		menu3Panel.SetActive (false);
		hardSelectorPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPlayButtonClickedInMenu1Panel(){
		menu1Panel.SetActive (false);
		menu2Panel.SetActive (true);
	}

	public void OnPlayButtonClickedInMenu2Panel(){
		hardSelectorPanel.SetActive (true);
	}

	public void OnEasySelected(){
		menu2Panel.SetActive (false);
		menu3Panel.SetActive (true);
	}

	public void OnMediumSelected(){
		menu2Panel.SetActive (false);
		menu3Panel.SetActive (true);
	}

	public void OnHardSelected(){
		menu2Panel.SetActive (false);
		menu3Panel.SetActive (true);
	}

	public void OnLeoSelected(){
		Application.LoadLevel (1);
	}
}
