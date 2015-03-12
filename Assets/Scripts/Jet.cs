using UnityEngine;
using System.Collections;

public class Jet : MonoBehaviour {

	public GameObject jetBombPrefab;
	public GameObject firePointGO;

	public GameObject jetBodyGO;
	public GameObject jetExplosionGO;

	public int cycle = 5;

	public float distanceScale = 1;

	public float time;

	public Vector3 initPosition;
	private Vector3 firePosition;

	public int lifeValue = 5;

	// Use this for initialization
	void Start () {
	
		initPosition = gameObject.transform.position;
		firePosition = firePointGO.transform.position;
		FallBombDown ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(lifeValue<=0 && jetExplosionGO.activeSelf == false){
			jetExplosionGO.SetActive(true);
			jetBodyGO.SetActive(false);

			return;
		}
		time += Time.deltaTime;
		time %= cycle;

		float value = 2 * Mathf.PI * time / cycle;

		if(value < Mathf.PI/2 || value > 1.5f * Mathf.PI){
			gameObject.transform.localScale = new Vector3(-1,1,1);
		}else{
			gameObject.transform.localScale = new Vector3(1,1,1);
		}

		Vector3 p = initPosition;
		p.x += Mathf.Sin (value) * distanceScale;
		gameObject.transform.position = p;
	}

	void FallBombDown(){
		GameObject o = (GameObject) Instantiate (jetBombPrefab, new Vector3(firePointGO.transform.position.x,firePosition.y,firePosition.z), firePointGO.transform.rotation);
		//o.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x * 5,0,0);
		Invoke ("FallBombDown", 2.0f);
	}

	public void OnCollisionEnter(Collision collisionInfo){
		
		if(collisionInfo.gameObject.tag == "PlayerButtle"){
			lifeValue--;
		}
		
	}

	public void OnDesctory(){
		Destroy (gameObject);
	}
}
