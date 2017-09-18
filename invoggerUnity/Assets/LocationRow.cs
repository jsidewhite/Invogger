using UnityEngine;
using System.Collections;

public class LocationRow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LiveThenDie());
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.forward * Time.deltaTime * 3.0f;
	}



	private IEnumerator LiveThenDie()
	{
		yield return new WaitForSeconds(30f);

		Destroy(gameObject);

		yield return null;
	}
}
