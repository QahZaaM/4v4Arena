using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour {

	public float m_chargeSpeed = 200.0f;
	public float m_strafeSpeed = 2.0f;
	public Collider m_shieldCollider;
	public Collider m_swordCollider;
	public GameObject m_normalShield;
	public GameObject m_ultimateShield;
	public bool m_isCharging = false;
	public bool m_hasDealtDamage;

	private Health m_healthScript;
	private PlayerController m_playerScript;

	void Start() {
		m_healthScript = GetComponent<Health>();
		m_playerScript = GetComponent<PlayerController>();
	}

	void Update() {
		if(m_isCharging) {
			transform.position += transform.forward * Time.deltaTime * m_chargeSpeed;
		}
	}

	public void Ability() {
		m_hasDealtDamage = false;
		m_swordCollider.enabled = true;
	}

	public void ResetAbility() {
		m_hasDealtDamage = false;
		m_swordCollider.enabled = false;
	}

	public void Charge() {
		m_playerScript.m_disableMovement = true;
		m_isCharging = true;
		m_shieldCollider.enabled = true;
	}

	public void ResetCharge() {
		m_playerScript.m_disableMovement = false;
		m_isCharging = false;
		m_shieldCollider.enabled = false;
	}

	public void Ultimate() {
		m_normalShield.SetActive(false);
		m_ultimateShield.SetActive(true);
	}

	public void ResetUltimate() {
		m_normalShield.SetActive(true);
		m_ultimateShield.SetActive(false);
	}
}
