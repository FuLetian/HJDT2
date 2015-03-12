using UnityEngine;
using System.Collections;

public class JetBombExplosion : MonoBehaviour {

	private bool isFirstLoop = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnAnimationStart(){
		if(isFirstLoop){
			isFirstLoop = false;
		}else{
			JetBomb jb = gameObject.transform.parent.GetComponent<JetBomb>();
			jb.OnDestory();
		}
	}
}
