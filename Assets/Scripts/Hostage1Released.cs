using UnityEngine;
using System.Collections;

public class Hostage1Released : MonoBehaviour {

	private Hostage1 parentGOScript;

	private bool isFirstTime;

	// Use this for initialization
	void Start () {
	
		parentGOScript = transform.parent.gameObject.GetComponent<Hostage1> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RoleRun(){
		parentGOScript.RoleRun ();
	}

	public void AnimationEnd(){
		if(isFirstTime == true){
			parentGOScript.AnimationEnd ();
		}else{
			isFirstTime = true;
		}
	}

	public void StopRun(){
		parentGOScript.StopRun ();
	}
}
