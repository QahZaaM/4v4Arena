using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnableScripts : NetworkBehaviour {

	public string m_character = "isWarrior"; // TODO: change to private when we have character select
	public GameObject m_mageAnim;
	public GameObject m_warriorAnim;
	public GameObject m_rogueAnim;
	public GameObject m_hunterAnim;

	private PlayerController m_playerScript;
	private Animator m_animationController;
	private NetworkAnimator m_networkAnimationController;
	private Animator m_currAnimator;

	void Start() {
		if(isLocalPlayer) {
			GetComponent<PlayerController>().enabled = true;
			m_currAnimator = GetComponent<Animator>();
			m_networkAnimationController = GetComponent<NetworkAnimator>();
			
			switch(m_character) {
				case "isMage":
					//GetComponent<Mage>().enabled = true;
					m_currAnimator.avatar = m_mageAnim.GetComponent<Animator>().avatar;
					break;
				case "isWarrior":
					//GetComponent<Warrior>().enabled = true;
					m_currAnimator.avatar = m_warriorAnim.GetComponent<Animator>().avatar;
					break;
				case "isRogue":
					//GetComponent<Rogue>().enabled = true;
					m_currAnimator.avatar = m_rogueAnim.GetComponent<Animator>().avatar;
					break;
				case "isHunter":
					//GetComponent<Hunter>().enabled = true;
					m_currAnimator.avatar = m_hunterAnim.GetComponent<Animator>().avatar;
					break;
				default:
					break;
			}
		}
	}

	public string getChar() {
		return m_character;
	}
}
