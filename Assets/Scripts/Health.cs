﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float m_maxHealth = 1000.0f;
	public float m_currentHealth;
	public float m_damageReduction = 1.0f;
	public Slider m_healthBar;
	public Text m_healthText;

	void Start() {
		m_currentHealth = m_maxHealth;
		UpdateHealthSlider();
		UpdateHealthText();
	}

	public void TakeDamage(float damage) {
		m_currentHealth -= damage * m_damageReduction;
		Die();
		UpdateHealthSlider();
		UpdateHealthText();
	}

	public void TakeDotDamage(float dotDmg, float ticks, float tickTime){
		StartCoroutine(Dot(dotDmg, ticks, tickTime));
	}

	public void Die() {
		if(m_currentHealth <= 0) {
			m_currentHealth = 0;
			// TODO: add what happens when they die
			Debug.Log("Dead");
		}
	}

	public void Heal(int amount) {
		m_currentHealth += amount;
		if(m_currentHealth > m_maxHealth) {
			m_currentHealth = m_maxHealth;
		}
		UpdateHealthSlider();
		UpdateHealthText();
	}

	private float CalculateHealth() {
		return m_currentHealth / m_maxHealth;
	}

	IEnumerator Dot(float dot, float tickNumber, float timePerTick){
		float appliedTimes = 0;

		yield return new WaitForSeconds(timePerTick);

		while(appliedTimes < tickNumber) {
			TakeDamage(dot);
			yield return new WaitForSeconds(timePerTick);
			appliedTimes++;
   	 	}
	}

	public void UpdateHealthSlider() {
		if (m_healthBar != null) {
			m_healthBar.value = CalculateHealth();
		}
	}

	public void UpdateHealthText() {
		if (m_healthText != null) {
			m_healthText.text = m_currentHealth + "/" + m_maxHealth;
		}
	}
}
