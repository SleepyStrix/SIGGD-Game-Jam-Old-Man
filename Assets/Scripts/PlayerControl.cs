using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    //for rendering/physics
	private Rigidbody playerRB;
	private GameObject walker;
	private GameObject walkerMoveTarget;
	private Rigidbody walkerRB;
	private GameObject cam;
	private bool walk = false;
	public float turnSpeed = 0.2f;
	public float walkSpeed = 0.2f;
	public float maxSpeed = .8f;
	private bool leftBlocked = false;
	private bool rightBlocked = false;
	public float moveDelay = 2f;
	public float walkDistance = 0.5f;
	private bool canRotate = true;
	public float maxWalkerDist = 3.5f;
	public bool alive = true;
	private AudioSource source;
	public AudioClip[] clipList;
	//public float timer = 0;

	// Use this for initialization
	void Start () {
			playerRB = GetComponent<Rigidbody> ();
			walker = GameObject.Find ("Walker");
			walkerMoveTarget = GameObject.Find ("WalkerMoveTarget");
			walkerRB = walker.GetComponent<Rigidbody> ();
			cam = GameObject.Find ("PlayerCam");
			source = GetComponent<AudioSource> ();
			//InvokeRepeating ("inputHandeling", 0f, 1f);
			StartCoroutine ("moveAll");
    }

    // Update is called once per frame
    void Update () {
		if (alive) {
			if (Input.GetButtonDown ("WalkForward")) {
				walk = true;
			}
			if (Input.GetButtonUp ("WalkForward")) {
				walk = false;
			}
			if (Input.GetButtonDown ("Grab")) {
			}
			if (Input.GetButtonDown ("Push")) {
				thrustWalker ();
			}
		}
	}


	void FixedUpdate() {
		if (alive) {
			//limit player speed
			if (playerRB.velocity.magnitude > maxSpeed) {
				playerRB.velocity = playerRB.velocity.normalized * maxSpeed;
			}

			//check if too far from walker
			tooFarFromWalker ();

			if (Vector3.Distance (transform.position, walkerMoveTarget.transform.position) < walkDistance + 1) {
				canRotate = true;
			} else {
				canRotate = false;
			}

			//--person forward movement--
			Vector3 player2DPos = new Vector3 (transform.position.x, 0, transform.position.z);
			Vector3 walker2DPos = new Vector3 (walkerMoveTarget.transform.position.x, 0, walkerMoveTarget.transform.position.z);
			//if (Vector3.Distance (transform.position, walkerMoveTarget.transform.position) > walkDistance) {
			if (Vector3.Distance (player2DPos, walker2DPos) > walkDistance) {
				//Ray dir = new Ray(transform.position, walkerMoveTarget.transform.position);
				//playerRB.AddForce((dir.direction) * maxSpeed, ForceMode.VelocityChange);
				//Quaternion look = Quaternion.LookRotation (walker.transform.position);
				playerRB.AddRelativeForce (new Vector3 (0, 0, walkSpeed), ForceMode.VelocityChange);
				//playerRB.MoveRotation (Quaternion.Euler (new Vector3 (0, look.eulerAngles.y, 0)));
				//playerRB.AddRelativeForce (new Vector3 (0, 0, walkSpeed), ForceMode.VelocityChange);

				//playerRB.AddForce (new Vector3(walkerMoveTarget.transform.position.x, 0, walkerMoveTarget.transform.position.z), ForceMode.VelocityChange);
			} else {
				playerRB.AddForce (Vector3.zero, ForceMode.VelocityChange);
			}
			//--player rotation--
			//turn right
			if (Input.GetAxis ("Horizontal") > 0) {

				if (!rightBlocked && !walk && canRotate) {
					transform.Rotate (0, turnSpeed, 0);
				}
			}
			//turn left
			if (Input.GetAxis ("Horizontal") < 0) {

				if (!leftBlocked && !walk && canRotate) {
					transform.Rotate (0, -turnSpeed, 0);
				}
			}
			//--camera rotation--
			float yRot = cam.transform.localRotation.eulerAngles.y;
			float xRot = cam.transform.localRotation.eulerAngles.x;
			float newYRot = Input.GetAxis ("Mouse X") + yRot;
			float newXRot = (-Input.GetAxis ("Mouse Y")) + xRot;
			//Debug.Log (newYRot);
			//Debug.Log (Input.GetAxis ("Mouse Y"));
			//limit horizontal rotation
			if ((newYRot > 65 && newYRot < 325) || (newYRot > 180 && newYRot < 65)) {
				//Debug.Log (newYRot);
				newYRot = yRot;
			}
			//limit vertical rotation
			if ((newXRot > 15 && newXRot < 180) || (newXRot < 340 && newXRot > 275)) {
				//Debug.Log (newXRot);
				newXRot = xRot;
			}
			cam.transform.localEulerAngles = new Vector3 (newXRot, newYRot, 0);
		}

	}

	public void inputHandeling() {
	}

	IEnumerator moveAll() {
		while (true) {
			while (walk) {
				if (Vector3.Distance (transform.position, walkerMoveTarget.transform.position) < walkDistance + 3.5f){
					moveWalker ();
				}
				yield return new WaitForSeconds (moveDelay);
			}
			yield return new WaitForSeconds (1f);
		}
	}

	public void moveWalker() {
		playOldManSound (false);
		walker.transform.position = transform.TransformPoint(new Vector3(0,1,1.5f)); //reset walker position
		walkerRB.AddRelativeForce (new Vector3 (0, 1.5f, 2), ForceMode.Impulse);

	}

	public void movePlayer() {
	}

	public void thrustWalker() {
		if (!walk) {
			if (Vector3.Distance (transform.position, walkerMoveTarget.transform.position) < walkDistance + 1) {
				playOldManSound (false);
				walker.transform.position = transform.TransformPoint(new Vector3(0,1,1.5f)); //reset walker position
				walkerRB.AddRelativeForce (new Vector3 (0, 4, 5), ForceMode.Impulse);
			}
		}
	}

    /*public int getEnergy()
    {
        return energy;
    }
    public int getHeartStat()
    {
        return heartStat;
    }
    public int getLeftEarBattery()
    {
        return leftEarBattery;
    }
    public int getRightEarBattery()
    {
        return rightEarBattery;
    }*/

	void leftIsBlocked() {
		leftBlocked = true;
	}
	void leftIsNotBlocked() {
		leftBlocked = false;
	}
	void rightIsBlocked() {
		rightBlocked = true;
	}
	void rightIsNotBlocked() {
		rightBlocked = false;
	}

	void tooFarFromWalker() {
		if (Vector3.Distance (transform.position, walkerMoveTarget.transform.position) > maxWalkerDist) {
			//playerRB.velocity = Vector3.zero;
			die ();
		}
	}
    
    //updates heart status based on..??
    //needs to add fixedUpdate for battery life based on actual time instead of frames

	public void kill() {
		die ();
	}

	void die() {
		playDeathSound (true);
		alive = false;
		Debug.Log ("you dead son");
		playerRB.AddRelativeForce(new Vector3(1,0,0), ForceMode.Impulse);
		playerRB.constraints = RigidbodyConstraints.None;
	}

	void playOldManSound(bool certain) {
		int change = Random.Range (0, 3);
		if ((change == 1 && alive) || certain) {
			int rand = Random.Range (0, 20);
			source.clip = clipList [rand];
			source.Play ();
		}
	}
	void playDeathSound(bool certain) {
		int change = Random.Range (11, 14);
		if ((change == 1 && alive) || certain) {
			int rand = Random.Range (0, 20);
			source.clip = clipList [rand];
			source.Play ();
		}
	}

}
