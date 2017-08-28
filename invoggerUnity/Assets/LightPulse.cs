using UnityEngine;
using System.Collections;

public class LightPulse : MonoBehaviour {

	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = Mathf.Sin(Time.time * 40f) * .5f;
	}
}
