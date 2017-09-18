﻿using UnityEngine;
using System.Collections;

public class KeyLocationLeader : MonoBehaviour {

	public GameObject start;
	public GameObject end;
	public float speedMultiplier = 0.75f;
	public GameObject rowLocationPrefab;
	
	private Vector3 leaderLocation;
	private GameObject currentRow;

	//	bool resetPosition = true;

	float percentage = 0f;

	// Use this for initialization
	void Start ()
	{
		leaderLocation = start.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//percentage += Time.deltaTime * 1.5f;
		percentage += Time.deltaTime * speedMultiplier;
		//leaderLocation = Vector3.Lerp(start.transform.position, end.transform.position, percentage % 1.0f);

		/*if (resetPosition)
			leaderLocation = start.transform.position;
			*/

		GetLeaderLocation();

		transform.FindChild("Tracker").transform.position = leaderLocation;
	}

	public Vector3 GetLeaderLocation()
	{
		leaderLocation = Vector3.Lerp(start.transform.position, end.transform.position, percentage % 1.0f);
		return leaderLocation;
	}

	public Transform GetRowTransform()
	{
		return currentRow.transform;
	}

	public void ResetPosition()
	{
		//resetPosition = true;
		//leaderLocation = start.transform.position;
		percentage = 0f;
		//currentRow = GameObject.CreatePrimitive(PrimitiveType.Cube);
		currentRow = (GameObject)Instantiate(rowLocationPrefab, GetLeaderLocation(), Quaternion.identity);
	}
}
