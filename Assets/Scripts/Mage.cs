using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mage : NetworkBehaviour {

	public GameObject m_fireBallPrefab;
	public GameObject m_polyPrefab;
	public GameObject m_blackHolePrefab;
	public GameObject m_oneHandSpellSpawn;
	public GameObject m_blackHoleSpawn;
	public float m_bulletSpeed = 20.0f;

	[Command]
	void CmdFire(GameObject prefab, GameObject spawnPosition) {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (prefab, spawnPosition.transform.position, spawnPosition.transform.rotation);

		NetworkServer.Spawn(bullet);
			
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * m_bulletSpeed;
	}

	public void MageAuto() {
		if(isLocalPlayer){
			CmdFire(m_fireBallPrefab, m_oneHandSpellSpawn);
		}
	}

	public void Polymorph() {
		if(isLocalPlayer){
			CmdFire(m_polyPrefab, m_oneHandSpellSpawn);
		}
	}

	public void Blackhole() {
		if(isLocalPlayer){
			CmdFire(m_blackHolePrefab, m_blackHoleSpawn);
		}
	}
}
