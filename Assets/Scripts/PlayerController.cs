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
	//private Animator m_animationController;

	void Awake() {
		m_controller = GetComponent<CharacterController>();
		//m_animationController = GetComponent<Animator>();
	}

	void Update() {
		if(!m_disableMovement) {
			if(m_grounded) {
				// get the direction im strafing
				if(!(Input.GetAxis("Horizontal") != 0)) {
					m_moveDirection.x -= Input.GetAxis("Strafing");
				}

				// apply the movement direction
				m_moveDirection *= m_speed * m_speedMultiplier;

				// if i press space, jump
				if(Input.GetButton("Jump")) {
					//m_animationController.SetTrigger("Jump");
					//m_soundMgr.PlayJump();
					m_moveDirection.y = m_jumpSpeed;
					
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

				// if(m_moveDirection.magnitude > 0.0f) {
				// 	m_animationController.SetBool("isRunning", true);
				// }

				// m_animationController.SetFloat("Forward", m_moveDirection.z);
				// m_animationController.SetFloat("Right", m_moveDirection.x);

				// get my world location and apply the moveDirection to it
				m_moveDirection = transform.TransformDirection(m_moveDirection);
			}
		}

		// TODO: For Maaz when he does camera controller for camera rotation turning the player
		// if(Input.GetMouseButton(1)){
		// 	transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
		// } else{
		// 	transform.Rotate(0,Input.GetAxis("Horizontal") * m_turnSpeed * Time.deltaTime, 0);
		// }

		// apply gravity and check if im on the ground
		m_moveDirection.y -= m_gravity * Time.deltaTime;
		m_grounded = ((m_controller.Move(m_moveDirection * Time.deltaTime)) & CollisionFlags.Below) != 0;
		Debug.Log (((m_controller.Move(m_moveDirection * Time.deltaTime)) & CollisionFlags.Below) != 0);

		//reset jumping after grounded
		m_isJumping = m_grounded ? false : m_isJumping;

		// TODO: add when we have jumping animations
		// if(m_isJumping) {
		// 	m_animationController.SetTrigger("Jump");
		// }
	}
}
