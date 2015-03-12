using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

	public GameObject firePointGO;
	public GameObject tankFirePrefab;
	public GameObject topGO;

	public GameObject aliveGO;
	public GameObject explosionGO;

	public float fireLoopTime;

	private float lastFireTime;
	private bool isTopAnimation;
	private float timeCounter;
	private Vector3 topOriginPosition;
	public int alivePoint = 3;

	// Use this for initialization
	void Start () {
	
		topOriginPosition = topGO.transform.position;

		aliveGO.SetActive (true);
		explosionGO.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if(alivePoint <= 0){
			aliveGO.SetActive (false);
			explosionGO.SetActive (true);
			return;
		}
	
		lastFireTime += Time.deltaTime;

		if(lastFireTime > fireLoopTime){
			Fire();
			lastFireTime = 0f;
		}

		if(isTopAnimation){
			timeCounter += Time.deltaTime;

			topGO.transform.position = new Vector3(topOriginPosition.x + Mathf.Sin(Mathf.PI*timeCounter/0.5f)*0.1f,topOriginPosition.y,topOriginPosition.z);

			if(timeCounter > 0.5f){
				isTopAnimation = false;
				timeCounter = 0f;
				topGO.transform.position = topOriginPosition;
			}
		}

	}

	void Fire(){
		isTopAnimation = true;
		Instantiate (tankFirePrefab, this.firePointGO.transform.position, this.firePointGO.transform.rotation);
	}

	public void OnCollisionEnter(Collision collisionInfo){
		
		if(collisionInfo.gameObject.tag == "bomb"){
			alivePoint -- ;
		}
	}

	public void OnDestory(){
		Destroy (gameObject);
	}
}
