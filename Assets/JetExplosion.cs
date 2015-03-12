using UnityEngine;
using System.Collections;

public class JetExplosion : MonoBehaviour {

	private bool isFirstLoop = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnFisrtFrame(){
		if(isFirstLoop == false){
			gameObject.transform.parent.GetComponent<Jet>().OnDesctory();
		}

		isFirstLoop = false;
	}
}
