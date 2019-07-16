using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
	private Animation anim;
	public GameObject bullet;
	private Vector3 spawn;
	private bool active;
	private AudioSource aSource;
	public AudioClip a1;
	public AudioClip a2;
	public AudioClip a3;
	private bool reloading;
	public UnityEngine.UI.Text ammo;

	void Start()
	{
		PlayerPrefs.SetInt("mag", 7);
		anim = gameObject.GetComponent<Animation>();
		anim["ScopeIn"].layer=0;
		anim["Recoil"].layer=1;
		anim["run"].layer=2;
		anim["adsRecoil"].layer=3;
		active=false;
		aSource = gameObject.GetComponent<AudioSource>();
	}
    
    void Update()
    {
		if (PlayerPrefs.GetInt("mag")>0)
		{
			ammo.text = PlayerPrefs.GetInt("mag").ToString();
		}
		else
		{
			ammo.text = "0";
		}

		if (Camera.main.fieldOfView==5)
		{
			GameObject.Find("/Canvas/Scope").GetComponent<UnityEngine.UI.Image>().color = Color.white;
			spawn = new Vector3(0.9267682f, 4.247596f, 1.911633f);
		}
		else
		{
			GameObject.Find("/Canvas/Scope").GetComponent<UnityEngine.UI.Image>().color = Color.clear;
			spawn = new Vector3(0.04704066f, 210.1038f, -0.5612374f);
		}

		if ((Input.GetButtonDown("Fire2")) && (!anim.IsPlaying("Recoil")))
		{
			anim["ScopeIn"].speed = 1;
			anim.Play("ScopeIn");
			active=true;
		}

		if ((Input.GetButtonUp("Fire2")) && (active==true))
		{
			anim["ScopeIn"].speed = -1;
			anim["ScopeIn"].time = 0.05f;
			anim.Play("ScopeIn");
			active=false;
		}

		if ((Input.GetButtonDown("Fire1")) && (PlayerPrefs.GetInt("mag")>0))
		{
			if (Camera.main.fieldOfView==5)
			{
				anim.Play("adsRecoil");
			}
			else
			{
				anim.Play("Recoil");
			}
			aSource.clip = a1;
			aSource.pitch = 2;
			aSource.Play();
		}
		else if (Input.GetButtonDown("Fire1"))
		{
			aSource.clip = a3;
			aSource.pitch = 2;
			aSource.Play();
		}

		if ((Input.GetButtonUp("Fire1")) && (PlayerPrefs.GetInt("mag")>=0))
		{
			GameObject temp = Instantiate(bullet) as GameObject;
			temp.transform.parent = GameObject.Find("/Sniper/Main/barrel").transform;
			temp.transform.localPosition = spawn;
			temp.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			temp.transform.localScale = new Vector3(5f, 5f, 5f);
			temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
			temp.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
			temp.name = "bullet";
			if (PlayerPrefs.GetInt("mag")==0)
			{
				PlayerPrefs.SetInt("mag", -1);
			}
		}

		if ((Input.GetKeyDown("r")) && (Camera.main.fieldOfView!=5) && (PlayerPrefs.GetInt("mag") < 7) && (reloading == false))
		{
			PlayerPrefs.SetInt("mag", -1);
			aSource.clip = a2;
			aSource.pitch = 1;
			aSource.Play();
			StartCoroutine(reload());
		}
		else if ((Input.GetKeyDown("r")) && (PlayerPrefs.GetInt("mag") < 7) && (reloading == false))
		{
			anim["ScopeIn"].speed = -1;
			anim["ScopeIn"].time = 0.05f;
			anim.Play("ScopeIn");
			active=false;
			PlayerPrefs.SetInt("mag", -1);
			aSource.clip = a2;
			aSource.pitch = 1;
			aSource.Play();
			StartCoroutine(reload());
		}

		if ((GameObject.Find("Player").GetComponent<PlayerMovement>().moveDirection.x != 0) && (GameObject.Find("Player").GetComponent<PlayerMovement>().characterController.isGrounded))
		{
			anim.Play("run");
		}
    }

	IEnumerator reload()
	{
		reloading = true;
		yield return new WaitForSeconds(2);
		PlayerPrefs.SetInt("mag", 7);
		reloading = false;
	}
}