using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController characterController;
	public Vector3 moveDirection = Vector3.zero;
	private GameObject Sniper;
	private float speed = -15f;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		Sniper = GameObject.Find("Sniper");
	}

	void Update()
	{
		if (characterController.isGrounded)
		{
			moveDirection = transform.TransformDirection(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))*speed;
			if (Input.GetButton("Jump"))
			{
				moveDirection.y = 8f;
			}
			if (Input.GetButtonDown("Crouch"))
			{
				if (characterController.height==5f)
				{
					speed = -5f;
					characterController.height = 4f;
					characterController.center = new Vector3(1f, -0.5f, 2f);
				}
				else
				{
					speed = -15f;
					characterController.height = 5f;
					characterController.center = new Vector3(1f, 0f, 2f);
				}
			}
		}
		transform.eulerAngles = new Vector3(0, Sniper.transform.eulerAngles.y, 0);
		moveDirection.y -= 20f * Time.deltaTime;
		characterController.Move(moveDirection * Time.deltaTime);
	}
}