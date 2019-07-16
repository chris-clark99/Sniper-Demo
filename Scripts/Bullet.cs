using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int mag = 7;
	public GameObject hole;

    void Update()
    {
		mag = PlayerPrefs.GetInt("mag");
		if ((Input.GetButtonDown("Fire1")) && (PlayerPrefs.GetInt("mag")>0))
		{
			mag--;
			PlayerPrefs.SetInt("mag", mag);
			transform.parent = null;
			gameObject.GetComponent<Rigidbody>().useGravity = true;
			gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 1000000);
			gameObject.GetComponent<Light>().enabled = true;
			this.enabled = false;
		}
    }
	void OnCollisionEnter(Collision collision)
	{
		GameObject temp = Instantiate(hole) as GameObject;
		temp.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x+90, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z+90);
		temp.transform.position = gameObject.transform.position;
		temp.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<Renderer>().material.color;
		if ((collision.gameObject.GetComponent<FixedJoint>() == null) && (collision.gameObject.name != "Player"))
		{
			collision.gameObject.AddComponent<FixedJoint>();
		}
		temp.GetComponent<FixedJoint>().connectedBody = collision.gameObject.GetComponent<Rigidbody>();
		Destroy(gameObject);
	}
}
