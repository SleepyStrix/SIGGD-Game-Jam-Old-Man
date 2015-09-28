using UnityEngine;
using System.Collections;

public class RobotAI : MonoBehaviour {
	public Transform player;
	public float fov = 60;
	
	public Transform patrolPointSet;
	private Transform[] patrolPoints;
	public float patrolPointReachedThreshold = 1.0f;
	public AudioSource source;
	private int currentPoint = 0;
	
	private NavMeshAgent agent;
	private MonoBehaviour flasherScript;
	
	public bool follow;
	
	void Start () {
		StartCoroutine ("SoundDelay");
		player = GameObject.Find ("Player").transform;
 		agent = GetComponent<NavMeshAgent>();
		source = GetComponentInChildren<AudioSource>();
		flasherScript = transform.FindChild ("Flasher").gameObject.GetComponent<MonoBehaviour> ();
		patrolPoints = patrolPointSet.GetComponentsInChildren<Transform>();
	}
	
	void Update () {

		if (player == null) {
			follow = false;
			//Debug.LogWarning("No player to follow.");
		}
		
		if (player != null && (follow || IsPlayerInView())) {
			agent.destination = player.position;
		} else {
			if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < patrolPointReachedThreshold) {
				currentPoint++;
				if (currentPoint >= patrolPoints.Length) {
					currentPoint = 0;
				}
			}
			agent.destination = patrolPoints[currentPoint].position;
		}
	}

	IEnumerator SoundDelay() {
		//Debug.Log ("calling");
		while (true) {
			yield return new WaitForSeconds(1f);
			playEnemySound();
			yield return new WaitForSeconds(Random.Range(5.0f, 6.0f));
		}
	}
	
	bool IsPlayerInView () {
		if (Vector3.Angle(player.position - transform.position, transform.forward) < fov) {
			RaycastHit hitInfo = new RaycastHit();
			return Physics.Linecast(transform.position, player.position, out hitInfo);
		} else {
			return false;
		}
	}

	void playEnemySound() {
		flasherScript.Invoke ("PlayAttackingAudio", 1);

	}
}
