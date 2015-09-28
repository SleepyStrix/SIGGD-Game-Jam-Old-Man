using UnityEngine;
using System.Collections;

public class turnLeftCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.isTrigger == false) {
			SendMessageUpwards ("leftIsBlocked");
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.isTrigger == false) {
			SendMessageUpwards("leftIsNotBlocked");
		}
	}
}
