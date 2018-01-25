using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnableScripts : NetworkBehaviour {

	void Start() {
		if(isLocalPlayer) {
			GetComponent<PlayerController>().enabled = true;
		}
	}
}
