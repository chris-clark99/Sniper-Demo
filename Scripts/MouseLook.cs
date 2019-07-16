using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	private float sensitivityX;
	private float sensitivityY;

	private float rotationY;
	private float rotationX;

	private bool active;

	private Vector3 axis;

	void Start ()
	{
		active = true;
	}

	void Update ()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (active==true)
			{
				active=false;
			}
			else
			{
				active=true;
			}
		}
		if (active==true)
		{
			Cursor.visible = false;
			Screen.lockCursor = true;
			if (Camera.main.fieldOfView==60)
			{
				sensitivityX = 10f;
				sensitivityY = 10f;
			}
			else
			{
				sensitivityX = 0.5f;
				sensitivityY = 0.5f;
			}
			rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, -60f, 60f);
			transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
		}
		else
		{
			Cursor.visible = true;
			Screen.lockCursor = false;
		}
		axis = GameObject.Find("Player").transform.localPosition;
		if (GameObject.Find("Player").GetComponent<PlayerMovement>().characterController.height==5f)
		{
			transform.localPosition = new Vector3(axis.x, axis.y+1f, axis.z);
		}
		else
		{
			transform.localPosition = new Vector3(axis.x, axis.y, axis.z);
		}
	}
}