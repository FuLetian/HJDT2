using UnityEngine;
using System.Collections;

public class Hostage2Released : MonoBehaviour {

	private Hostage2 parent;
	private bool isFirstLoop;
	// Use this for initialization
	void Start () {
	
		isFirstLoop = true;
		parent = transform.parent.GetComponent<Hostage2> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRun(){
		parent.Run ();
	}

	public void OnStop(){
		parent.Stop ();
	}

	public void OnDestory(){
		if(isFirstLoop){
			isFirstLoop = false;
		}else{
			parent.HostageDestory();
		}
	}
}
