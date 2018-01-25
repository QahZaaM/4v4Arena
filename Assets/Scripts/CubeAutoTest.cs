using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeAutoTest : NetworkBehaviour {

	public GameObject m_fireBallPrefab;
	public GameObject m_spellSpawn;

	private NetworkAnimator m_networkAnimator;
	private Animator m_animator;

	void Start() {
		//m_networkAnimator = GetComponent<NetworkAnimator>();
		//m_animator = GetComponent<Animator>();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)) {
			//m_animator.SetBool("isAttacking", true);
			Fire();
		}
	}

	[Command]
	void CmdFire() {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (m_fireBallPrefab, m_spellSpawn.transform.position, m_spellSpawn.transform.rotation);

		NetworkServer.Spawn(bullet);
			
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
		
	}

	public void Fire() {
		if(isLocalPlayer){
			CmdFire();
		}
	}
}
