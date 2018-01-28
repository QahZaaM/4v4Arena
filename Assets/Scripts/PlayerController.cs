using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float m_speed = 5.0f;
	public float m_strafeSpeed = 5.0f;
	public float m_speedMultiplier = 1.0f;
	public float m_gravity = 20.0f;
	public float m_jumpSpeed = 8.0f;
	public float m_moveBackwardsMultiplier = 0.8f;
	public bool m_disableMovement = false;
	public Image[] m_CDMasks;

	private bool m_isJumping = false;
	private bool m_grounded = false;
	private bool m_movementOnCD = false;
	private bool m_abilityOnCD = false;
	private bool m_canUlt = false;
	private Vector3 m_moveDirection = Vector3.zero;
	private CharacterController m_controller;
	private Animator m_animationController;
	private NetworkEnableScripts m_networkEnableScript;
	private float m_abilityCDTime = 5.0f;
	private float m_movementCDTime = 10.0f;
	private int m_ultimateProgress = 0;
	

	void Awake() {
		m_controller = GetComponent<CharacterController>();
		m_animationController = GetComponent<Animator>();
		m_networkEnableScript = GetComponent<NetworkEnableScripts>();
	}

	void Start() {
		Camera.main.GetComponent<CameraController>().m_target = transform;
		m_animationController.SetBool(m_networkEnableScript.m_character, true);
	}

	void Update() {

		// check if you can ult and caps it at 100
		if(m_ultimateProgress >= 100) {
			m_ultimateProgress = 100;
			m_canUlt = true;
		}

		if(!m_disableMovement) {
			if(m_grounded) {
				ResetJump();
				m_moveDirection = new Vector3(Input.GetAxis("Strafing"), 0, Input.GetAxis("Vertical"));

				// apply the movement direction
				m_moveDirection *= m_speed * m_speedMultiplier;

				// if i press space, jump
				if(Input.GetButtonDown("Jump")) {
					if(!m_isJumping) {
						m_animationController.SetBool("Jump", true);
						m_isJumping = true;
						m_moveDirection.y = m_jumpSpeed;
						//m_soundMgr.PlayJump();
					}	
				}

				if(Input.GetMouseButtonDown(0)) {
					m_animationController.SetBool("isAttacking", true);
				}

				if (Input.GetKeyDown(KeyCode.Alpha1) && !m_abilityOnCD) {
					m_animationController.SetBool("isAbility", true);
					StartCoroutine(CoolDownSystem(m_abilityCDTime, "Ability"));
				}

				if(Input.GetKeyDown(KeyCode.Alpha2) && !m_movementOnCD) {
					m_animationController.SetBool("isMovement", true);
					StartCoroutine(CoolDownSystem(m_movementCDTime, "Movement"));
				}

				if(Input.GetKeyDown(KeyCode.Alpha3) && m_canUlt) {
					m_animationController.SetBool("isUlt", true);	
					m_canUlt = false;
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
		m_grounded = ((m_controller.Move(m_moveDirection * Time.deltaTime)) & CollisionFlags.Below) != 0;

		// reset jumping after grounded
		m_isJumping = m_grounded ? false : m_isJumping;
	}

	// resets the jump variables at the end of the jump animation
	public void ResetJump() {
		m_animationController.SetBool("Jump", false);
		m_isJumping = false;
	}

	IEnumerator CoolDownSystem(float cooldownvalue, string Ability) {	
		yield return new WaitForSeconds(cooldownvalue);
		if(Ability == "Ability") {
			while(m_abilityOnCD) {
				m_CDMasks[0].fillAmount -= Time.deltaTime / cooldownvalue;

				if(m_CDMasks[0].fillAmount == 0) {
					m_abilityOnCD = false;
				}
				yield return null;
			}
		}

		if(Ability == "Movement") {
			while(m_movementOnCD) {
				m_CDMasks[1].fillAmount -= Time.deltaTime / cooldownvalue;

				if(m_CDMasks[1].fillAmount == 0) {
					m_movementOnCD = false;
				}
				yield return null;
			}
		}
	}

	IEnumerator UltimateProgress() {
		yield return new WaitForSeconds(1);
		m_ultimateProgress++;
	}

	public void ResetAttack() {
		m_animationController.SetBool("isAttacking", false);
	}
}
