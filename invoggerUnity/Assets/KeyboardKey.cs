using UnityEngine;
using System.Collections;
using TMPro;

public class KeyboardKey : MonoBehaviour {

	public TextMeshPro textMesh;
	public Light light;


	private bool dying = false;
	private GameObject model = null;

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
		if (null == textMesh)
			return;

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
		try
		{
			model = transform.FindChild("container").FindChild("model").FindChild("Cube").gameObject;
		}
		catch
		{
		}
		//StartCoroutine(Shimmer());
		StartCoroutine(LiveThenDie());
	}

	// Update is called once per frame
	void Update()
	{
		if (dying)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(-5, -20, 10));
			GetComponent<Rigidbody>().AddTorque(new Vector3(10, 0, 0));
			if (light != null)
				light.intensity = Mathf.Lerp(light.intensity, 0f, Time.deltaTime * 1f);
		}
	}

	private IEnumerator LiveThenDie()
	{
		StartCoroutine(Shimmer());
		yield return new WaitForSeconds(5.0f);
		dying = true;
		//GetComponent<Rigidbody>().AddTorque(new Vector3(1, 2, 3));
		//GetComponent<Rigidbody>().AddForce(new Vector3(10000, 2000, 3000));

		//Color yellow = new Color(0.9f, 0.9f, 0.1f);
		//model.GetComponent<Renderer>().material.SetColor("_EmissionColor", );
		//StartCoroutine(Shimmer());

		yield return new WaitForSeconds(4f);

		Destroy(gameObject);

		yield return null;
	}

	private float rate = 0f;
	private IEnumerator Shimmer()
	{
		if (model != null)
		{
			Color yellow = new Color(0.3f, 0.3f, 0.3f, 0.4f);
			//Color orig = model.GetComponent<Renderer>().material.GetColor("_EmissionColor");
			Color orig = model.GetComponent<Renderer>().material.GetColor("_Color");
			//Color orig = new Color(0.0f, 0.9f, 1.0f);
			Color c;
			rate = 0.1f;

			// Duplicate the material
			//Material newMat = new Material(model.GetComponent<Renderer>().material);

			/*
			Material newMat = new Material(model.GetComponent<Renderer>().material.shader);
			model.GetComponent<Renderer>().material = newMat;
			*/

			/*
			Shader newShader = new Shader();
			newShader.*/
			float time = 0f;

			StartCoroutine(RateIncreaseFunction());


			while (true)
			{
				//rate += 0.1f;
				rate += 0.02f;
				time += Time.deltaTime;
				c = Color.Lerp(orig, yellow, (Mathf.Cos(time * rate + Mathf.PI) + 1) / 2f);
				//c = Color.Lerp(orig, yellow, 0f);

				//model.GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
				model.GetComponent<Renderer>().material.SetColor("_Color", c);
				//model.GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
				//model.GetComponent<Renderer>().material.SetColor("_Color", c);

				yield return null;
			}
		}
		yield return null;
	}

	private IEnumerator RateIncreaseFunction()
	{
		yield return new WaitForSeconds(1.5f);
		rate += 4f;
		yield return new WaitForSeconds(1.5f);
		rate += 14f;
	}
}
