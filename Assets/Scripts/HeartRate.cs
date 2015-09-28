using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartRate : MonoBehaviour {
	public float rate = 90.0f;
	public float rateChange = 1.0f;
	public float normalRate = 90.0f;
	public float maxRate = 120.0f;
	public float minRate = 50.0f;
	
	public Text rateDisplay;

	public float fov = 60f;

	void Start () {
	
	}
	
	void Update () {
		if (rate < minRate || rate > maxRate) OnDied();
		else {
			int ev = EnemiesVisible();
			if (ev > 0) rate += rateChange * ev * ev;
			else if (rate < normalRate) rate += rateChange;
			else if (rate > normalRate) rate -= rateChange;
			
			if (rateDisplay != null) rateDisplay.text = Mathf.Round(rate).ToString();
		}
	}

	public void OnDied () {
		SendMessage ("kill");
	}

	int EnemiesVisible () {
		int n = 0;
		foreach (RobotAI r in FindObjectsOfType<RobotAI>()) {
			if (Vector3.Angle(r.transform.position - transform.position, transform.forward) < fov) {
				RaycastHit hitInfo = new RaycastHit();
				if (Physics.Linecast(transform.position, r.transform.position, out hitInfo) && hitInfo.collider == r.GetComponent<Collider>()) {
					n++;
				}
			}
		}
		return n;
	}
}
