using UnityEngine;
using System.Collections;

using System.Runtime.InteropServices;

public class LettersIncoming : MonoBehaviour {


	[DllImport("invologgerDll", EntryPoint = "GetKey")]
	public static extern int GetKey();

	[DllImport("invologgerDll", EntryPoint = "SetHooks")]
	public static extern int SetHooks();

	[DllImport("invologgerDll", EntryPoint = "Unhook")]
	public static extern int Unhook();




	public GameObject keyPrefab;
	public GameObject keyInvokePrefab;
	public GameObject keyActionStopPrefab;

	public KeyLocationLeader keyLocationLeader;
	private KeyCode keyCode;
	float timeSinceLastKey = 99f;


	// Use this for initialization
	void Start()
	{
		SetHooks();
	}

	void OnDestroy()
	{
		if (98 == Unhook())
		{
			throw new System.Exception("unhook failed");
		}

		throw new System.Exception("unhook worked");
	}

	

	// Update is called once per frame
	void Update () {
		timeSinceLastKey += Time.deltaTime;

		int key = GetKey();
		if (654 != key)
		{
			if (timeSinceLastKey > 1.2f)
			{
				timeSinceLastKey = 0;
				keyLocationLeader.ResetPosition();
			}

			char theKey = (char)key;
			//Instantiate(keyPrefab, new Vector3(1.0f, 1.0f, 0), Quaternion.identity);

			GameObject thePrefab;
			if (theKey == 'R')
			{
				thePrefab = keyInvokePrefab;
			}
			else if(theKey == 'S')
			{
				thePrefab = keyActionStopPrefab;
			}
			else
			{
				thePrefab = keyPrefab;
			}

			GameObject keyInstance = (GameObject)Instantiate(thePrefab, keyLocationLeader.GetLeaderLocation(), Quaternion.identity);
			keyInstance.GetComponent<KeyboardKey>().SetKey(key);
		}
		/*
		if (GetKeyDownAndWhich())
		{
			//Instantiate(keyPrefab, new Vector3(1.0f, 1.0f, 0), Quaternion.identity);
			GameObject newKeyfab = (GameObject)Instantiate(keyPrefab, keyLocationLeader.GetLeaderLocation(), Quaternion.identity);
			newKeyfab.GetComponent<KeyboardKey>().SetKey(keyCode);
		}
		*/
	}

	private bool GetKeyDownAndWhich()
	{
		keyCode = KeyCode.None;
		if (Input.GetKeyDown(KeyCode.H))
		{
			keyCode = KeyCode.H;
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			keyCode = KeyCode.S;
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			keyCode = KeyCode.Q;
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			keyCode = KeyCode.W;
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			keyCode = KeyCode.E;
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			keyCode = KeyCode.R;
		}

		if (keyCode != KeyCode.None)
		{ return true; }

		return false;
	}
}
