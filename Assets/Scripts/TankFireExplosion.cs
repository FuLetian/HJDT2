using UnityEngine;
using System.Collections;

public class TankFireExplosion : MonoBehaviour {

	private bool isFirstLoop = true;

	private Tank tankScrpit;
	// Use this for initialization
	void Start () {
	
		tankScrpit = gameObject.transform.parent.GetComponent<Tank> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimationStar(){

		if(isFirstLoop){
			isFirstLoop = false;
		}else{
			tankScrpit.OnDestory();
		}
	}
}
