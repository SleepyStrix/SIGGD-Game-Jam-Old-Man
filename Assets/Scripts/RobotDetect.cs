using UnityEngine;
using System.Collections;

public class RobotDetect : MonoBehaviour {
	private RobotAI robotAI;

	void Start () {
		robotAI = transform.parent.GetComponent<RobotAI>();
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Player") {
			robotAI.follow = true;
			robotAI.Invoke("playEnemySound", 1);
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.gameObject.name == "Player") {
			robotAI.follow = false;
		}
	}
}
