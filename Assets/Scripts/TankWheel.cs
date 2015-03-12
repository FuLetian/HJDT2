using UnityEngine;
using System.Collections;

public class TankWheel : MonoBehaviour {

	public float speed;
	public float total;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		total += Time.deltaTime;
		total %= 5;
		float result = Mathf.Cos (360 * (total/5.0f)) * speed;


	}
}
