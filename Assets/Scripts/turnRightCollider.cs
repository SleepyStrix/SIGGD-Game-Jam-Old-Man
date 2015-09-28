using UnityEngine;
using System.Collections;

public class turnRightCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.isTrigger == false) {
			SendMessageUpwards ("rightIsBlocked");
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.isTrigger == false) {
			SendMessageUpwards("rightIsNotBlocked");
		}
	}
}
