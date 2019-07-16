using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(destruct());
	}

	IEnumerator destruct()
	{
		yield return new WaitForSeconds(60);
		Destroy(gameObject);
	}
}
