using UnityEngine;
using System.Collections;

public class KeyDie : MonoBehaviour {

	private bool dying = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(LiveThenDie());
	}
	
	// Update is called once per frame
	void Update () {
		if (dying)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(-5, -20, 10));
			GetComponent<Rigidbody>().AddTorque(new Vector3(10, 0, 0));
		}
	}

	private IEnumerator LiveThenDie(	)
	{
		yield return new WaitForSeconds(0.6f);
		dying = true;
		//GetComponent<Rigidbody>().AddTorque(new Vector3(1, 2, 3));
		//GetComponent<Rigidbody>().AddForce(new Vector3(10000, 2000, 3000));

		yield return new WaitForSeconds(1f);

		Destroy(gameObject);

		yield return null;
	}
}
