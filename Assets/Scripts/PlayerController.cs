using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float m_speed = 5.0f;
	public float m_strafeSpeed = 5.0f;
	public float m_speedMultiplier = 1.0f;
	public float m_gravity = 20.0f;
	public float m_jumpSpeed = 8.0f;
	public float m_moveBackwardsMultiplier = 0.8f;
	public bool m_disableMovement = false;

	private bool m_isJumping = false;
	private bool m_grounded = false;
	private Vector3 m_moveDirection = Vector3.zero;
	private CharacterController m_controller;
	private Animator m_animationController;

	void Awake() {
		m_controller = GetComponent<CharacterController>();
		m_animationController = GetComponent<Animator>();
	}

	void Start() {
		Camera.main.GetComponent<CameraController>().m_target = transform;
	}

	void Update() {
		if(!m_disableMovement) {
			if(m_grounded) {
				ResetJump();
				m_moveDirection = new Vector3(Input.GetAxis("Strafing"), 0, Input.GetAxis("Vertical"));

				// apply the movement direction
				m_moveDirection *= m_speed * m_speedMultiplier;

				// if i press space, jump
				if(Input.GetButton("Jump")) {
					if(m_animationController.GetBool("Jump") == false) {
						m_animationController.SetBool("Jump", true);
						//m_soundMgr.PlayJump();
					}
					Debug.Log(m_animationController.GetBool("Jump"));
				}

				// if(m_Disengage) {
				// 	m_moveDirection.y = m_jumpSpeed;
				// 	m_moveDirection.z = -m_jumpSpeed;
				// 	m_Disengage = false;
				// } else {	
				// 	m_animationController.SetBool("Movement",false);
				// }

				// if(m_grounded) {
				// 	m_Disengage = false;
				// }

				// tells the animation controller the direction im moving
				m_animationController.SetFloat("Forward", m_moveDirection.z);
				m_animationController.SetFloat("Right", m_moveDirection.x);

				// get my world location and apply the moveDirection to it
				m_moveDirection = transform.TransformDirection(m_moveDirection);
			}
		}

		// apply gravity and check if im on the ground
		m_moveDirection.y -= m_gravity * Time.deltaTime;
		m_grounded = ((m_controller.Move(m_moveDirection * Time.deltaTime)) & CollisionFlags.Below) !=0;

		//reset jumping after grounded
		m_isJumping = m_grounded ? false : m_isJumping;
	}

	public void ResetJump() {
		m_animationController.SetBool("Jump", false);
	}
}
