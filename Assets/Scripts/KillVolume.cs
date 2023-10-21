using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class KillVolume : MonoBehaviour {

	
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PlayerManager>().TouchDamage(200);
		}

	}
	
}
