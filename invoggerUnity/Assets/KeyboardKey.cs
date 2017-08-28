using UnityEngine;
using System.Collections;
using TMPro;

public class KeyboardKey : MonoBehaviour {

	public TextMeshPro textMesh;
	public Light light;


	private bool dying = false;

	// The imported function
	//[DllImport("invologgerDll", EntryPoint = "fninvologgerDll")]
	//public static extern int fninvologgerDll(int);
	

	//public static extern void TestSort(int[] a, int length);

	public void SetKey(KeyCode keyCode)
	{
		//textMesh.text = keyCode.ToString();
		//textMesh.SetText(keyCode.ToString());
		//textMesh.SetText("f : " + fninvologgerDll(5));
		//sdf(5);
		//textMesh.SetText("f : " + sdf());
		//textMesh.SetText("f : " + GetKey());
		textMesh.SetText("f : " + keyCode);
	}

	public void SetKey(int key)
	{
		//textMesh.text = keyCode.ToString();
		//textMesh.SetText(keyCode.ToString());
		//textMesh.SetText("f : " + fninvologgerDll(5));
		//sdf(5);
		//textMesh.SetText("f : " + sdf());
		//textMesh.SetText("f : " + GetKey());
		//textMesh.SetText("f : " + key);
		char theChar = (char)key;
		textMesh.SetText("" + theChar);

		Color color = Color.white;
		if (theChar == 'Q')
			color = new Color(.05f, .1f, .8f);
		else if (theChar == 'W')
			color = new Color(.8f, .2f, .8f);
		else if (theChar == 'E')
			color = new Color(.8f, 0.6f, .2f);
		else if (theChar == 'R')
			color = new Color(.45f, .15f, .05f);
		else if (theChar == 'D')
			color = new Color(.5f, .5f, .5f);
		else if (theChar == 'F')
			color = new Color(.5f, .5f, .5f);
		else if (theChar == 'S')
			color = Color.red;
		else if (theChar == 'H')
			color = Color.red;
		else
			color = new Color(0, 0, 0);

		textMesh.color = color;
		textMesh.GetComponent<Renderer>().material.SetColor("_GlowColor", color);
	}

	// Use this for initialization
	void Start()
	{
		StartCoroutine(LiveThenDie());
	}

	// Update is called once per frame
	void Update()
	{
		if (dying)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(-5, -20, 10));
			GetComponent<Rigidbody>().AddTorque(new Vector3(10, 0, 0));
			light.intensity = Mathf.Lerp(light.intensity, 0f, Time.deltaTime * 1f);
		}
	}

	private IEnumerator LiveThenDie()
	{
		yield return new WaitForSeconds(0.6f);
		dying = true;
		//GetComponent<Rigidbody>().AddTorque(new Vector3(1, 2, 3));
		//GetComponent<Rigidbody>().AddForce(new Vector3(10000, 2000, 3000));
		yield return null;
	}
}
